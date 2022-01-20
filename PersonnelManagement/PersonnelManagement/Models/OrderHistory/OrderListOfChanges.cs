using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Models
{
    public class PositionOperation
    {
        public int positionType { get; set; }
        public int positionRank { get; set; }
        public bool customCategory { get; set; }
        public int positionSource { get; set; }
        public (bool, bool, bool) operation { get; set; }
        public Dictionary<DateTime?, double> activateDates { get; set; }

        public PositionOperation(Decreeoperation operation, Repository repository)
        {
            Position position = repository.PositionsLocal()[operation.Subject];
            this.positionType = position.Positiontype;
            this.positionRank = position.Cap.GetValueOrDefault() != 0 ? position.Cap.GetValueOrDefault() : position.Positioncategory;
            this.customCategory = position.Cap.GetValueOrDefault() != 0 ? false : true;
            this.positionSource = position.Sourceoffinancing;
            this.operation = (Convert.ToBoolean(operation.Created), Convert.ToBoolean(operation.Changed), Convert.ToBoolean(operation.Deleted));
            activateDates = new Dictionary<DateTime?, double> { };
            if (operation.Datecustom == 1)
                activateDates[operation.Dateactive] = repository.PositionsLocal()[operation.Subject].Partval;
        }

        public void merge_dates(PositionOperation obj)
        {
            foreach (DateTime? i in obj.activateDates.Keys)
                if (!this.activateDates.ContainsKey(i))
                    this.activateDates[i] = 0;
            foreach (KeyValuePair<DateTime?, double> i in obj.activateDates)
                this.activateDates[i.Key] += i.Value;
        }

        public override int GetHashCode()
        {
            int hashpositionType = this.positionType.GetHashCode();
            int hashpositionRank = this.positionRank.GetHashCode();
            int hashpositionSource = this.positionSource.GetHashCode();
            int item = (this.operation.Item1 ? 1 : 0) * 100;
            int hashpositionCr = item.GetHashCode();
            item = (this.operation.Item2 ? 1 : 0) * 10000;
            int hashpositionCh = item.GetHashCode();
            item = (this.operation.Item3 ? 1 : 0) * 100000;
            int hashpositionDE = item.GetHashCode();

            return hashpositionType ^ hashpositionRank ^ hashpositionSource ^ hashpositionCr ^ hashpositionCh ^ hashpositionDE;
        }

        public override bool Equals(Object obj)
        {
            PositionOperation obj1 = obj as PositionOperation;
            if (obj1.positionType == this.positionType &&
                obj1.positionRank == this.positionRank &&
                obj1.positionSource == this.positionSource &&
                obj1.operation.Item1 == this.operation.Item1 &&
                obj1.operation.Item2 == this.operation.Item2 &&
                obj1.operation.Item3 == this.operation.Item3)
                return true;
            return false;
        }
    }

    public class ChangeDecreeItem
    {
        public string name { get; set; }
        public string rankname { get; set; }
        public string add_number { get; set; }
        public string remove_number { get; set; }
        public string source_flag { get; set; }
        public string note { get; set; }

        public ChangeDecreeItem(string name="",
            string rankname="",
            string add="",
            string remove="",
            string source="",
            string note="")
        {
            this.name = name;
            this.rankname = rankname;
            this.add_number = add;
            this.remove_number = remove;
            this.source_flag = source;
            this.note = note;
        }
    }

    public class DecreeChanges
    {
        public Structure m_structure;
        public List<DecreeChanges> m_children_changes;

        public List<ChangeDecreeItem> m_changes;
        public List<ChangeDecreeItem> m_structure_changes;
        public bool m_structure_custom_changes;
        public bool m_structure_custom_deleted;
        public Structure m_privios_structure;

        public Dictionary<PositionOperation, double> m_positions_changes;

        public List<Decreeoperation> m_operations;

        public double forgote_operations;
        public double addition_operations;

        public DecreeChanges(Structure structure)
        {
            m_structure = structure;
            m_children_changes = new List<DecreeChanges> { };
            m_changes = new List<ChangeDecreeItem> { };
            m_structure_changes = new List<ChangeDecreeItem> { };
            m_structure_custom_changes = false;
            m_structure_custom_deleted = false;
            m_privios_structure = new Structure();

            m_positions_changes = new Dictionary<PositionOperation, double> { };

            m_operations = new List<Decreeoperation> { };

            forgote_operations = 0;
            addition_operations = 0;
        }

        private void Plus_counters(DecreeChanges changes)
        {
            this.forgote_operations += changes.forgote_operations;
            this.addition_operations += changes.addition_operations;
        }

        public void appendChildren(DecreeChanges item)
        {
            m_children_changes.Add(item);
            this.Plus_counters(item);
        }

        public void appendOperations(List<Decreeoperation> items, Repository repository)
        {
            Dictionary<Decreeoperation, int> for_structure_operations = new Dictionary<Decreeoperation, int> { };
            foreach(Decreeoperation i in items)
            {
                if (i.Subject < 0)
                    for_structure_operations[i] = 1;
                else
                    m_positions_changes = analizePositionDecreeOperation(i, repository, m_positions_changes);
            }
            m_structure_changes = analizeStructureDecreeOperation(for_structure_operations, repository, m_structure_changes);
            m_changes = formationPositionsOutput(m_positions_changes, repository, m_changes);
        }

        private Dictionary<PositionOperation, double> analizePositionDecreeOperation(Decreeoperation operation, Repository repository, Dictionary<PositionOperation, double> input_output)
        {
            PositionOperation time_operations = new PositionOperation(operation, repository);
            double partval = repository.PositionsLocal()[operation.Subject].Partval;
            if (input_output.ContainsKey(time_operations))
            {
                if(time_operations.activateDates.Count != 0)
                {
                    List<KeyValuePair<PositionOperation, double>> for_merge_l = input_output.ToList().Where(s => s.Key.GetHashCode() == time_operations.GetHashCode()).ToList();
                    KeyValuePair<PositionOperation, double> for_merge = for_merge_l.First();
                    input_output.Remove(for_merge.Key);
                    for_merge.Key.merge_dates(time_operations);
                    input_output[for_merge.Key] = partval + for_merge.Value;
                }
                input_output[time_operations] += partval;
            }
            else
                input_output[time_operations] = partval;
            if (time_operations.operation.Item1)
                this.addition_operations += partval;
            if (time_operations.operation.Item3)
                this.forgote_operations += partval;
            return input_output;
        }

        private List<ChangeDecreeItem> analizeStructureDecreeOperation(Dictionary<Decreeoperation, int> operation, Repository repository, List<ChangeDecreeItem> input_out)
        {
            foreach (Decreeoperation i in operation.Keys)
                input_out = analizeStructureDecreeOperation(i, repository.StructuresLocal()[Math.Abs(i.Subject)], repository, input_out);
            return input_out;
        }

        private List<ChangeDecreeItem> analizeStructureDecreeOperation(Decreeoperation operation, Structure structure, Repository repository, List<ChangeDecreeItem> input_out)
        {
            if(repository.GetOriginalStructure(structure) == repository.GetOriginalStructure(m_structure))
            {
                string rank_city = getRankAndCity(structure);
                if(rank_city != "")
                {
                    input_out.Add(new ChangeDecreeItem(name: rank_city));
                    //input_out.Add(new ChangeDecreeItem());
                }
                string note = operation.Datecustom == 1 ? "c " + operation.Dateactive.GetValueOrDefault().ToString("dd.MM.yyyy") : "";
                input_out.Add(new ChangeDecreeItem(name: getActionByOperation(operation),
                                                   note: note));
                if (operation.Changed == 1)
                {
                    input_out.Add(new ChangeDecreeItem());
                    /*m_structure_custom_changes = true;
                    input_out.Add(new ChangeDecreeItem(name: repository.StructuresLocal()[operation.Changedtype].Name,
                                                       note: note));*/
                    m_privios_structure = repository.StructuresLocal()[operation.Changedtype];
                    m_structure_custom_changes = true;
                    input_out.Add(new ChangeDecreeItem(name: structure.Name,
                                                       note: note));
                }
                else if(operation.Deleted == 1)
                    m_structure_custom_deleted = true;
            }
            return input_out;
        }

        private string getActionByOperation(Decreeoperation operation)
        {
            return (operation.Created == 1) ? "(создается)" :
                ((operation.Changed == 1) ? "(переименовывается в)" :
                ((operation.Deleted == 1 && operation.Subject < 0) ? "(упраздняется)" :
                ((operation.Deleted == 1 && operation.Subject > 0) ? "(сокращается)" : "")));
        }

        public static string getRankAndCity(Structure structure)
        {
            string rank = (structure.Rank != 0) ? structure.Rank.ToString() + "-й разряд" : "";
            string city = (structure.City != null) ? structure.City.ToString() : "";
            if (rank != "" && city != "")
                return "(" + rank + ", " + city + ")";
            return rank != "" ? "(" + rank + ")" : (city != "" ? city : "");
        }

        private List<ChangeDecreeItem> formationPositionsOutput(Dictionary<PositionOperation, double> positions, Repository repository, List<ChangeDecreeItem> input_out)
        {
            List<KeyValuePair<PositionOperation, double>> list = positions.ToList();
            list.Sort((a, b) => repository.PositiontypesLocal()[b.Key.positionType].Priority.CompareTo(repository.PositiontypesLocal()[a.Key.positionType].Priority));
            foreach(KeyValuePair<PositionOperation, double> i in list)
            {
                string note = "";
                foreach (KeyValuePair<DateTime?, double> j in i.Key.activateDates)
                    note += (note != "" ? ";\t" : "") + j.Value.ToString() + " c " + j.Key.GetValueOrDefault().ToString("dd.MM.yyyy");
                input_out.Add(new ChangeDecreeItem());
                input_out.Add(new ChangeDecreeItem(name: repository.PositiontypesLocal()[i.Key.positionType].Name,
                    rankname: !i.Key.customCategory ? repository.RanksLocal()[i.Key.positionRank].Name.ToLower() : repository.PositioncategoriesLocal()[i.Key.positionRank].Name.ToLower(),
                    add: i.Key.operation.Item1 ? i.Value.ToString() : "-",
                    remove: i.Key.operation.Item3 ? i.Value.ToString() : "-",
                    source: stringToLetter(repository.SourceoffinancingsLocal()[i.Key.positionSource].Name),
                    note: note));
            }
            return input_out;
        }

        private string stringToLetter(string input)
        {
            string output = "";
            foreach (string i in input.Split(" "))
                output += char.ToUpper(i[0]);
            return output;
        }
    }

    public class OrderListOfChanges
    {
        private Repository m_repository;
        private string m_session_id;
        private User m_user;

        private Decree m_actual_decree;
        private List<Decreeoperation> m_list_of_changes;

        private MemoryStream m_stream;

        private DecreeChanges m_resualt;

        public OrderListOfChanges(Repository repository)
        {
            m_repository = repository;
            m_stream = new MemoryStream();
        }

        public void InitializeChangesList(IEnumerable<Decreeoperation> list_of_changes, string session_id)
        {
            if (!checkAccessToUser(session_id))
                return;
            m_list_of_changes = list_of_changes.ToList();
            pickOutDecree();

            formationResualtStructure();

            DocTemplateToListOfChanges.CreateDocument(m_stream, m_repository, m_resualt, m_actual_decree, m_user);
        }

        public MemoryStream getStream()
        {
            m_stream.Position = 0;
            return m_stream;
        }

        public void InitializeChangesList(Decree decree, string session_id)
        {
            if (decree.Historycal == 1)
                return;
            if (!checkAccessToUser(session_id))
                return;
            m_actual_decree = decree;
            m_list_of_changes = m_repository.DecreeoperationsLocal().Values.Where(s => s.Decree == m_actual_decree.Id).ToList();
            m_list_of_changes.Sort((a, b) => (a.Id).CompareTo((b.Id)));

            formationResualtStructure();

            DocTemplateToListOfChanges.CreateDocument(m_stream, m_repository, m_resualt, m_actual_decree, m_user);
        }

        private void pickOutDecree()
        {
            Dictionary<int, int> all_decrees = new Dictionary<int, int> { };
            foreach (Decreeoperation i in m_list_of_changes)
            {
                if (all_decrees.ContainsKey(i.Decree))
                    all_decrees[i.Decree] += 1;
                else
                    all_decrees[i.Decree] = 1;
            }
            int max = all_decrees.Max(s => s.Value);
            m_actual_decree = m_repository.DecreesLocal()[all_decrees.Where(kvp => kvp.Value == max).Select(kvp => kvp.Key).First()];
        }

        private bool checkAccessToUser(string session_id)
        {
            m_session_id = session_id;
            if (PersonnelManagement.Services.IdentityService.IsLogined(session_id, m_repository))
            {
                m_user = PersonnelManagement.Services.IdentityService.GetUserBySessionID(session_id, m_repository);
                if (PersonnelManagement.Services.IdentityService.canEditStructures(session_id, m_repository))
                    return true;
            }
            return false;
        }

        private void formationResualtStructure()
        {
            Dictionary<Structure, List<Decreeoperation>> changes_by_structures = new Dictionary<Structure, List<Decreeoperation>> { };
            foreach (Decreeoperation i in m_list_of_changes)
            {
                (Structure time_structure, int id) = getActualStructure(i, m_user.Date);
                time_structure = time_structure != null ? time_structure : m_repository.StructuresLocal()[id];
                if (changes_by_structures.ContainsKey(time_structure))
                    changes_by_structures[time_structure].Add(i);
                else
                    changes_by_structures[time_structure] = new List<Decreeoperation> { i };
            }
            formationStructureTree(changes_by_structures);
        }

        private (Structure, int) getActualStructure(Decreeoperation decree_operation, DateTime? date)
        {
            if (decree_operation.Subject < 0)
                return (m_repository.GetActualStructureInfo(Math.Abs(decree_operation.Subject), date.GetValueOrDefault()), Math.Abs(decree_operation.Subject));
            Position time_position = m_repository.PositionsLocal()[decree_operation.Subject];
            return (m_repository.GetActualStructureInfo(time_position.Structure, date.GetValueOrDefault()), Math.Abs(time_position.Structure));
        }

        private void formationStructureTree(Dictionary<Structure, List<Decreeoperation>> input)
        {
            List<List<Structure>> time = new List<List<Structure>> { };
            int min_len = int.MaxValue;
            foreach (KeyValuePair<Structure, List<Decreeoperation>> i in input)
            {
                time.Add(getParentsStructures(i.Key));
                if (time.Last().Count < min_len)
                    min_len = time.Last().Count;
            }
            min_len = 0;
            var (general_structure, to_calculation) = findGeneralStrucrure_by_decreeOperations(time, min_len);
            m_resualt = AddStructureTree(general_structure, to_calculation, input, new DecreeChanges(general_structure));
        }

        private DecreeChanges AddStructureTree(Structure general_structure, List<List<Structure>> structures, Dictionary<Structure, List<Decreeoperation>> structures_operations, DecreeChanges resualt)
        {
            if (structures_operations.ContainsKey(general_structure))
                resualt.appendOperations(structures_operations[general_structure], m_repository);
            foreach(KeyValuePair<Structure, List<List<Structure>>> i in dischargeUniqueStructures(structures))
            {
                resualt.appendChildren(AddStructureTree(i.Key, i.Value, structures_operations, new DecreeChanges(i.Key)));
            }
            return resualt;
        }

        private List<KeyValuePair<Structure, List<List<Structure>>>> dischargeUniqueStructures(List<List<Structure>> input)
        {
            Dictionary<Structure, List<List<Structure>>> output = new Dictionary<Structure, List<List<Structure>>> { };
            foreach(List<Structure> i in input)
            {
                if (i.Count == 0)
                    continue;
                if (output.ContainsKey(i[0]))
                    output[i[0]].Add(i.Skip(1).ToList());
                else
                    output[i[0]] = new List<List<Structure>> { i.Skip(1).ToList() };
            }
            List<KeyValuePair<Structure, List<List<Structure>>>> real_output = output.ToList();
            real_output.Sort((a, b) => a.Key.Priority.CompareTo(b.Key.Priority));
            return real_output;
        }

        private (Structure, List<List<Structure>>) findGeneralStrucrure_by_decreeOperations(List<List<Structure>> structures, int check_value)
        {
            if (check_value - 1 < 0)
                return ( new Structure(), structures );
            Structure previus_structure = structures.First()[check_value - 1];
            foreach(List<Structure> i in structures)
            {
                if (i[check_value - 1] != previus_structure)
                    return findGeneralStrucrure_by_decreeOperations(structures, check_value - 1);
            }
            foreach(List<Structure> i in structures)
                i.RemoveRange(0, check_value);

            return ( previus_structure, structures );
        }

        private List<Structure> getParentsStructures(Structure input)
        {
            if (input.Parentstructure == 0 || input.Parentstructure == null)
                return new List<Structure> { input };
            List<Structure> current = getParentsStructures(m_repository.StructuresLocal()[input.Parentstructure]);
            Structure st = m_repository.GetActualStructureInfo(input, m_user.Date.GetValueOrDefault());
            st = st != null ? st : m_repository.GetOriginalStructure(input);
            current.Add(st);
            return current;
        }
    }
}
