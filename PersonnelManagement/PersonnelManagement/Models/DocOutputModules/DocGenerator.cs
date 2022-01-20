using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using PersonnelManagement.USERS;
//using DocumentFormat.OpenXml.Spreadsheet;

namespace PersonnelManagement.Models
{
    public class SourceNote
    {
        public Sourceoffinancing m_source;
        public double number;

        public Dictionary<DateTime?, double> m_date_activation;

        public SourceNote(Sourceoffinancing source,
            DateTime? date_activation,
            double plus)
        {
            m_source = source;
            number = plus;
            m_date_activation = new Dictionary<DateTime?, double> { };
            m_date_activation[date_activation] = plus;
        }

        public bool isAppending(Sourceoffinancing source,
            DateTime? date_activation,
            double plus)
        {
            if (m_source != source)
                return false;
            number += plus;
            if (m_date_activation.ContainsKey(date_activation))
                m_date_activation[date_activation] += plus;
            else
                m_date_activation[date_activation] = plus;
            return true;
        }

        public void clearDictionary(DateTime? minimul_date)
        {
            if (minimul_date == null)
                return;
            Dictionary<DateTime?, double> time_dictionary = new Dictionary<DateTime?, double> { };
            foreach (DateTime? i in m_date_activation.Keys)
            {
                if (minimul_date.GetValueOrDefault().Ticks < i.GetValueOrDefault().Ticks)
                    time_dictionary[i] = m_date_activation[i];
            }
            m_date_activation = time_dictionary;
        }
    }

    public class staffTree
    {
        public Structure m_structure;

        public List<Position> m_positions;

        public List<staffTree> m_children_structure;

        public Dictionary<int, List<Position>> m_unique_positions;
        public Dictionary<int, List<SourceNote>> m_source_note;

        public Dictionary<int, double> m_del_positions_by_structure;

        public Dictionary<Positioncategoryrank, double> m_total_special_type { get; set; }
        public Dictionary<Positioncategoryrank, Dictionary<int, double>> m_special_type_source { get; set; }

        public staffTree(Structure structure,
            List<Position> positions_list,
            Dictionary<int, List<SourceNote>> source_note)
        {
            m_unique_positions = new Dictionary<int, List<Position>> { };
            m_source_note = source_note;

            m_del_positions_by_structure = new Dictionary<int, double> { };
            m_total_special_type = null;
            m_special_type_source = null;

            m_structure = structure;
            setPositions(positions_list);
            m_children_structure = null;
        }
        
        private void setPositions(List<Position> position_list)
        {
            m_positions = position_list;
            rebasePositions();
        }

        public void appendChildren(staffTree staff)
        {
            if(m_children_structure == null)
            {
                m_children_structure = new List<staffTree>();
            }
            m_children_structure.Add(staff);
        }

        private void rebasePositions()
        {
            foreach(Position i in m_positions)
            {
                double dels = 1 - i.Partval;
                if (m_del_positions_by_structure.ContainsKey(i.Positiontype))
                {
                    if (checkRealPositionLocation(i))
                        m_del_positions_by_structure[i.Positiontype] += i.Partval;
                    else
                        m_del_positions_by_structure[i.Positiontype] += dels;


                }
                else
                {
                    m_del_positions_by_structure[i.Positiontype] = 0;
                    if (checkRealPositionLocation(i))
                        m_del_positions_by_structure[i.Positiontype] += i.Partval;
                    else
                        m_del_positions_by_structure[i.Positiontype] += dels;
                }

                if (m_unique_positions.ContainsKey(i.Positiontype))
                    m_unique_positions[i.Positiontype].Add(i);
                else 
                {
                    List<Position> time_pos = new List<Position>();
                    time_pos.Add(i);
                    m_unique_positions[i.Positiontype] = time_pos;
                }
            }
        }

        private bool checkRealPositionLocation(Position pos)
        {
            if (m_structure.Changeorigin == 0 || m_structure.Changeorigin == null)
            {
                if (pos.Structure != m_structure.Id)
                    return true;
            }
            else
            {
                if (pos.Structure != m_structure.Changeorigin)
                    return true;
            }
            return false;
        }
    }
    public class StaffForDoc
    {
        public Order m_first_order;
        public List<Order> full_order_history;
        public Structure m_actual_structure;
        public staffTree m_full_staff;

        public User m_user;

        public StaffForDoc()
        {
            m_first_order = new Order();
            m_actual_structure = new Structure();
            full_order_history = new List<Order>();
        }

        public staffTree setstaffTree(Repository repository, User user, Structure actual_structure)
        {
            List<Position> toFunction = repository.GetPositions(repository.GetOriginalStructure(actual_structure).Id, user.Date.GetValueOrDefault()).ToList();
            toFunction = chechActual(repository, toFunction, user);
            //List<Position> toFunction = repository.GetPositions(actual_structure.Id, user.Date.GetValueOrDefault()).ToList();
            staffTree time_stafftree = new staffTree(repository.GetActualStructureInfo(actual_structure, user.Date.GetValueOrDefault()), toFunction, GenerateNoteByPositions(repository, toFunction, actual_structure));
            //staffTree time_stafftree = new staffTree(actual_structure, toFunction, GenerateNoteByPositions(repository, toFunction, actual_structure));
            List<Structure> children_list = repository.GetChildrenList(repository.GetOriginalStructure(actual_structure).Id);
            children_list = chechActual(repository, children_list, user);
            children_list.Sort((a, b) => (a.Priority).CompareTo((b.Priority)));
            foreach (Structure i in children_list)
            {
                //time_stafftree.appendChildren(setstaffTree(repository, user, i));
                if(!checkDuplicateStructures(time_stafftree, repository.GetActualStructureInfo(i, user.Date.GetValueOrDefault())))
                    time_stafftree.appendChildren(setstaffTree(repository, user, repository.GetActualStructureInfo(i, user.Date.GetValueOrDefault())));
            }
            return time_stafftree;
        }

        private List<Position> chechActual(Repository repository, List<Position> positions, User user)
        {
            List<Position> output_positions = new List<Position> { };
            foreach (Position i in positions)
            {
                List<Decreeoperation> time_decree_operation_deleted = repository.DecreeoperationsLocal().Values.Where(s =>
                s.Subject == i.Id &&
                s.Deleted == 1 &&
                s.Dateactive.GetValueOrDefault().Ticks < user.Date.GetValueOrDefault().Ticks &&
                repository.DecreesLocal()[s.Decree].Signed == 1).ToList();
                if (time_decree_operation_deleted != null && time_decree_operation_deleted.Count > 0)
                    continue;
                output_positions.Add(i);
                    
            }
            return output_positions;
        }

        private List<Structure> chechActual(Repository repository, List<Structure> structures, User user, int multiplier = -1)
        {
            List<Structure> output_structures = new List<Structure> { };
            foreach (Structure i in structures)
            {
                Structure time = repository.GetActualStructureInfo(i, user.Date.GetValueOrDefault());
                if (time == null)
                    continue;
                if (output_structures.Where(s => s.Id == time.Id).Count() > 0)
                    continue;
                output_structures.Add(time);
            }
            return output_structures;
        }

        private bool checkDuplicateStructures(staffTree tree, Structure new_structure)
        {
            if (tree.m_children_structure != null && new_structure == null)
            {
                foreach (staffTree i in tree.m_children_structure)
                {
                    if (i.m_structure == new_structure)
                        return true;
                }
            }
            if (new_structure == null)
                return true;
            return false;
        }

        public string getOrderName()
        {
            string output = this.m_first_order.decree.Nickname;
            output = "";
            output = regexpForPersoneName(output);
            output = regexpForMCHSName(output);
            output = regexpForMCHSDelDateNumber(output);
            return output;
        }

        private Dictionary<int, List<SourceNote>> GenerateNoteByPositions(Repository repo, List<Position> positions, Structure actual_structure)
        {
            Dictionary<int, List<SourceNote>> output = new Dictionary<int, List<SourceNote>> { };
            double counter_by_position = 1;
            foreach (Position i in positions)
            {
                counter_by_position = i.Partval;
                //var k = repo.GetOriginalStructure(actual_structure);
                if (i.Structure != repo.GetOriginalStructure(actual_structure).Id)
                    counter_by_position = 0;

                DateTime? time_date = new DateTime?();
                foreach (DecreeoperationManagement j in m_first_order.orders.Where(x => x.Subject == i.Id))
                {
                    time_date = j.Dateactive.GetValueOrDefault();
                    break;
                }
                if (time_date == null)
                    time_date = m_first_order.decree.Dateactive.GetValueOrDefault();
                if (output.ContainsKey(i.Positiontype))
                {
                    bool flag_appendin = false;
                    foreach (SourceNote j in output[i.Positiontype])
                    {
                        if (j.isAppending(repo.SourceoffinancingsLocal()[i.Sourceoffinancing],
                                          time_date,
                                          counter_by_position))
                        {
                            flag_appendin = true;
                            break;
                        }
                    }
                    if (!flag_appendin)
                    {
                        SourceNote time_source = new SourceNote(repo.SourceoffinancingsLocal()[i.Sourceoffinancing],
                                                                time_date,
                                                                counter_by_position);
                        output[i.Positiontype].Add(time_source);
                    }
                }
                else
                {
                    SourceNote time_source = new SourceNote(repo.SourceoffinancingsLocal()[i.Sourceoffinancing],
                                                            time_date,
                                                            counter_by_position);
                    output[i.Positiontype] = new List<SourceNote> { time_source };
                }
            }
            return clearDictionary(output, m_first_order.decree.Dateactive.GetValueOrDefault());
        }

        private Dictionary<int, List<SourceNote>> clearDictionary(Dictionary<int, List<SourceNote>> dic, DateTime? time)
        {
            foreach(int i in dic.Keys)
            {
                sortedDicList(dic[i]);
                foreach(SourceNote j in dic[i])
                {
                    j.clearDictionary(time);
                }
            }
            return dic;
        }

        private void sortedDicList(List<SourceNote> dic_list)
        {
            dic_list.Sort((a, b) => a.m_source.Id.CompareTo(b.m_source.Id));
        }

        private string regexpForPersoneName(string name)
        {
            Regex regular_value_for_string = new Regex(@"(\s?[Пп]риказ\w*\s)");
            String actual_string = "приказ ";
            if (name == null)
                return "приказ";
            if (regular_value_for_string.Matches(name).Count > 0)
            {
                name = regular_value_for_string.Replace(name, actual_string);
            }
            else
            {
                name = actual_string + name;
            }
            return name;
        }

        private string regexpForMCHSName(string name)
        {
            Regex regular_value_for_string = new Regex(@"(\s?[Мм][Чч][Сс]\w*\s)");
            String actual_string = " Министерства по чрезвычайным ситуациям Республики Беларусь ";
            if (regular_value_for_string.Matches(name).Count > 0)
            {
                name = regular_value_for_string.Replace(name, actual_string);
            }
            else
            {
                name = name + actual_string;
            }
            return name;
        }

        private string regexpForMCHSDelDateNumber(string name)
        {
            Regex regular_value_for_string = new Regex(@"(\s?[Оо][Тт]\s)");
            MatchCollection positions = regular_value_for_string.Matches(name);
            if (positions.Count > 0)
                name = name.Substring(0, positions[0].Index);

            regular_value_for_string = new Regex(@"(\s?№)");
            positions = regular_value_for_string.Matches(name);
            if (positions.Count > 0)
                name = name.Substring(0, positions[0].Index);

            return name;
        }
    }

    public class DocGenerator
    {
        private Repository m_repository;
        private string m_session_id;
        private User m_user;

        private MemoryStream m_stream;

        private StaffForDoc m_staff;
        public DocGenerator(Repository repository)
        {
            m_repository = repository;
            m_stream = new MemoryStream();
        }

        public void InitializeStuffManagement(StaffManagement staffManagement, string session_id)
        {
            m_staff = new StaffForDoc();
            if (!checkAccessToUser(session_id, staffManagement))
                return;
            loadStaff(staffManagement);

            DocTemplate.CreateDocument(m_stream, m_repository, m_staff);
        }

        public MemoryStream getStream()
        {
            m_stream.Position = 0;
            return m_stream;
        }

        private void loadStaff(StaffManagement staff)
        {
            m_staff.m_actual_structure = m_repository.StructuresLocal()[staff.Id];
            if (staff.Realid != 0)
                m_staff.m_actual_structure = m_repository.StructuresLocal()[staff.Realid];

            m_staff.m_actual_structure = m_repository.GetActualStructureInfo(m_staff.m_actual_structure, m_user.Date.GetValueOrDefault());
            setFirstOrder(m_staff);
            putChildrenStructs(m_staff);
        }

        private void setFirstOrder(StaffForDoc staff)
        {
            OrderWorker worker = new OrderWorker(m_repository);

            worker.Initializ_user(m_session_id);
            worker.Initializ_structure(staff.m_actual_structure.Id);

            bool write_flag = false;
            staff.full_order_history = worker.get_orders();
            foreach (Order i in staff.full_order_history)
            {
                foreach(DecreeoperationManagement order in i.orders)
                {
                    write_flag = false;
                    if (staff.m_actual_structure.Id * (-1) == order.Subject)
                    {
                        write_flag = true;
                        break;
                    }
                }
                if(write_flag)
                {
                    if(staff.m_first_order.orders.Count == 0)
                    {
                        staff.m_first_order = i;
                        continue;
                    }

                    if (i.decree.Datesigned.Value.Ticks < staff.m_first_order.decree.Datesigned.Value.Ticks)
                        staff.m_first_order = i;
                }
            }
            staff.full_order_history.Reverse();
            return;
        }

        private void putChildrenStructs(StaffForDoc staff)
        {
            m_staff.m_full_staff = m_staff.setstaffTree(m_repository, m_user, staff.m_actual_structure);
            m_staff.m_full_staff.m_total_special_type = GetFullSpecialTypes(m_repository, staff.m_actual_structure, m_staff.m_full_staff, new Dictionary<Positioncategoryrank, double> { });
            m_staff.m_full_staff.m_special_type_source = GetFullSpecialTypesSource(m_repository, staff.m_actual_structure, m_staff.m_full_staff, new Dictionary<Positioncategoryrank, Dictionary<int, double>> { });
        }
        private bool checkAccessToUser(string session_id, StaffManagement staffManagement)
        {
            m_session_id = session_id;
            if (PersonnelManagement.Services.IdentityService.IsLogined(session_id, m_repository))
            {
                m_user = PersonnelManagement.Services.IdentityService.GetUserBySessionID(session_id, m_repository);
                m_staff.m_user = m_user;
                if (PersonnelManagement.Services.IdentityService.CanReadStructure(session_id, m_repository, staffManagement.Id))
                {
                    if (staffManagement.Type == PersonnelManagement.Keys.STAFF_MANAGEMENT_TYPE_ALL)
                        return true;
                }
            }
            return false;
        }

        private Dictionary<Positioncategoryrank, double> GetFullSpecialTypes(Repository repository, Structure actual_structure, staffTree actual_staff, Dictionary<Positioncategoryrank, double> special_type)
        {
            foreach(Position i in actual_staff.m_positions)
            {
                double number = i.Partval;
                if (i.Structure != repository.GetOriginalStructure(actual_structure).Id)
                    number = 0;
                special_type = checkPositionCategory(repository, i, number, special_type);
            }
            if (actual_staff.m_children_structure == null)
                return special_type;
            foreach(staffTree i in actual_staff.m_children_structure)
                special_type = GetFullSpecialTypes(repository, i.m_structure, i, special_type);
            return special_type;
        }

        private Dictionary<Positioncategoryrank, Dictionary<int, double>> GetFullSpecialTypesSource(Repository repository, Structure actual_structure, staffTree actual_staff, Dictionary<Positioncategoryrank, Dictionary<int, double>> special_type_source)
        {
            foreach (Position i in actual_staff.m_positions)
            {
                double number = i.Partval;
                if (i.Structure != repository.GetOriginalStructure(actual_structure).Id)
                    number = 0;
                special_type_source = checkPositionCategory(repository, i, number, special_type_source);
            }
            if (actual_staff.m_children_structure == null)
                return special_type_source;
            foreach (staffTree i in actual_staff.m_children_structure)
                special_type_source = GetFullSpecialTypesSource(repository, i.m_structure, i, special_type_source);
            return special_type_source;
        }

        private Dictionary<Positioncategoryrank, double> checkPositionCategory(Repository repository, Position position, double numder_appended, Dictionary<Positioncategoryrank, double> types)
        {
            if(position.Positioncategory != 0)
            {
                Positioncategoryrank time_category = repository.PositioncategoryRanksLocal()[repository.PositioncategoriesLocal()[position.Positioncategory].Categoryranklink];
                if (types.ContainsKey(time_category))
                    types[time_category] += numder_appended;
                else
                    types[time_category] = numder_appended;
            }
            return types;
        }

        private Dictionary<Positioncategoryrank, Dictionary<int, double>> checkPositionCategory(Repository repository, Position position, double numder_appended, Dictionary<Positioncategoryrank, Dictionary<int, double>> types)
        {
            if (position.Positioncategory != 0)
            {
                Positioncategoryrank time_category = repository.PositioncategoryRanksLocal()[repository.PositioncategoriesLocal()[position.Positioncategory].Categoryranklink];
                if (types.ContainsKey(time_category))
                {
                    if(types[time_category].ContainsKey(position.Sourceoffinancing))
                        types[time_category][position.Sourceoffinancing] += numder_appended;
                    else
                        types[time_category][position.Sourceoffinancing] = numder_appended;
                }
                else
                {
                    types[time_category] = new Dictionary<int, double> { [position.Sourceoffinancing] = numder_appended };
                }
            }
            return types;
        }
    }
}
