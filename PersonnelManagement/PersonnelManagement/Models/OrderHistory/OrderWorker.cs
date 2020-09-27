using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{

    public class Order
    {
        public Decree decree { get; set; }
        public List<DecreeoperationManagement> orders { get; set; }

        public String name { get; set; }

        public Order()
        {
            this.decree = new Decree();
            this.orders = new List<DecreeoperationManagement>();
            this.name = "";
        }
        public Order(Decree decree, DecreeoperationManagement order, String name = "")
        {
            this.decree = decree;
            this.orders = new List<DecreeoperationManagement>() { order };
            //this.orders.Add(order);
            this.name = name;
        }

        public void ConfigureDecreePersonsName()
        {
            this.ConfigureDecreeName();

            int create = 0;
            int delete = 0;
            foreach(DecreeoperationManagement i in orders)
            {
                create += i.Created;
                delete += i.Deleted;
            }

            this.regexpForPersoneName(this.decreeOperationDoing(create, delete));
        }

        private string decreeOperationDoing(int created, int deleted)
        {
            if (created == deleted)
                return "Изменена ";
            if (created > deleted)
                return "Введена ";
            if (created < deleted)
                return "Упрозднена ";
            return "";
        }

        private void regexpForPersoneName(string string_start)
        {
            Regex regular_value_for_string = new Regex(@"(\s?[Пп]риказ\w*\s)");
            String actual_string = string_start + "приказом ";
            if (regular_value_for_string.Matches(this.name).Count > 0)
            {
                this.name = regular_value_for_string.Replace(this.name, actual_string);
            }
            else
            {
                this.name = actual_string + this.name;
            }
        }

        public void ConfigureDecreeName()
        {
            this.name = decree.Nickname;
            configureDate();
            configureNumber();
            configureSpaces();

            configureOrdersDate();
        }

        private void configureDate()
        {
            if (decree.Datesigned.GetValueOrDefault() == null)
                return;
            Regex regular_value_for_date = new Regex(@"(от\W{1,2}\d{2}[\.]\d{2}[\.]\d{4}[\W]{0,1})");
            String actual_date = " от " + decree.Datesigned.Value.Date.ToString("dd.MM.yyyy");
            if (regular_value_for_date.Matches(this.name).Count > 0)
            {
                this.name = regular_value_for_date.Replace(this.name, actual_date);
            }
            else
            {
                this.name += actual_date;
            }
        }

        private void configureNumber()
        {
            if(String.IsNullOrEmpty(decree.Number))
                return;
            Regex regular_value_for_number = new Regex(@"(№[\s]{0,}[\d]+)");
            String actual_number = " №" + decree.Number;
            if(regular_value_for_number.Matches(this.name).Count > 0)
            {
                this.name = regular_value_for_number.Replace(this.name, actual_number);
            }
            else
            {
                this.name += actual_number;
            }
        }

        private void configureSpaces()
        {
            Regex regular_value_for_end_spaces = new Regex(@"(\s{2,})");
            if (regular_value_for_end_spaces.Matches(this.name).Count > 0)
            {
                this.name = regular_value_for_end_spaces.Replace(this.name, " ");
            }
            else
            {
                this.name += " ";
            }
        }

        private void configureOrdersDate()
        {
            List<DateTime?> all_dates = new List<DateTime?>();
            foreach (DecreeoperationManagement i in orders)
            {
                if (i.MetaDateActive == null || i.MetaDateActive.CompareTo(decree.Datesigned) == 0)
                    continue;
                if(all_dates.Count() != 0)
                {
                    if (0 != i.MetaDateActive.CompareTo(all_dates.Last()))
                        all_dates.Add(i.MetaDateActive);
                }
                else
                {
                    all_dates.Add(i.MetaDateActive);
                }
            }
            if (all_dates.Count == 0)
                return;
            this.name += " от";
            foreach(DateTime? i in all_dates)
            {
                this.name += " (" + i.Value.Date.ToString("dd.MM.yyyy") + ")";
            }
            this.name += ".";
        }
    }

    public class OrderWorker
    {
        private Repository m_repository;
        private string m_session_id;
        private User m_user;
        private List<Order> m_order { get; }

        public OrderWorker(Repository repository)
        {
            m_repository = repository;
            m_session_id = "";

            m_order = new List<Order>();
            m_order.Clear();
        }

        public List<Order> get_orders()
        {
            return m_order;
        }

        public void Initializ_history_decree_by_edit(Order old_order, DecreeHistroryElementToAppending template_new_decree)
        {
            if (template_new_decree.CheckObject() && check_user_level_access(template_new_decree.structure_id))
                editHistoryDecreeOperation(old_order, template_new_decree);
            updateRepositorey_init_structure(template_new_decree.structure_id);
        }

        public void Initializ_history_decree_by_structure(DecreeHistroryElementToAppending template_new_decree)
        {
            if (template_new_decree.CheckObject() && check_user_level_access(template_new_decree.structure_id))
                appendnewDecree_and_decreeOperation(template_new_decree);
            updateRepositorey_init_structure(template_new_decree.structure_id);
        }

        public void Initialize_order_structureId_for_remove(Order order, int id)
        {
            if (!check_user_level_access(id))
                return;
            removeHistoryDecreeOrDecreeOperetion(order: order, Original_structure: m_repository.GetOriginalStructure(id));
            updateRepositorey_init_structure(id);
        }

        public void Initializ_structure(int subject_id)
        {
            if(check_user_level_access(subject_id))
            {
                loadStructureOrders(subject_id);
                ConfigureOrders();
            }
        }
        public void Initializ_position(int position_id)
        {
            DateTime user_date = new DateTime().ToLocalTime();
            if (m_user.Date != null)
                user_date = m_user.Date.Value;
            if (check_user_level_access(m_repository.GetActualStructureInfo(m_repository.GetStructureByPositionID(position_id),
                                                                            user_date).Id))
            {
                loadPositionOrders(position_id);
            }

            foreach(Order i in m_order)
            {
                i.ConfigureDecreePersonsName();
            }
        }

        public void Initializ_user(string session_id)
        {
            m_session_id = session_id;
            m_user = Services.IdentityService.GetUserBySessionID(m_session_id, m_repository);
        }

        private bool check_user_level_access(int subject_id)
        {
            if (Services.IdentityService.IsLogined(m_session_id, m_repository))
            {
                if (m_repository.isAllowedToReadStructure(m_user, subject_id))
                    return true;
            }
            return false;
        }

        private void loadStructureOrders(int structure_id)
        {
            DecreeOperationsRequest decreeOperationsRequest = new DecreeOperationsRequest();
            decreeOperationsRequest.full_output_flag = true;
            decreeOperationsRequest.Detailed = 1;
            decreeOperationsRequest.RequestedDate = m_user.Date.GetValueOrDefault();
            List<Structure> all_structures = new List<Structure>();
            m_repository.GetChildrenAndSelfOrigin(m_repository.GetOriginalStructure(structure_id).Id, all_structures);
            int q;
            foreach (Structure i in all_structures)
            {
                foreach (Structure j in m_repository.GetAllStructureFromOneOriginal(m_repository.GetOriginalStructure(i)))
                {
                    AppendOreder(j,
                    decreeOperationsRequest);
                }
                AppendOreder(i,
                    decreeOperationsRequest);
                foreach (Position k in m_repository.GetAllPositions(i.Id))
                {
                    AppendOreder(k,
                    decreeOperationsRequest);
                }
            }
            SortByMetaDateActive(m_order);
        }

        private void loadPositionOrders(int position_id)
        {
            DecreeOperationsRequest decreeOperationsRequest = new DecreeOperationsRequest();
            decreeOperationsRequest.full_output_flag = true;
            decreeOperationsRequest.Detailed = 1;
            decreeOperationsRequest.RequestedDate = m_user.Date.GetValueOrDefault();
            Position original_position;

            original_position = m_repository.PositionsLocal()[position_id];
            foreach (Positionhistory i in m_repository.Positionhistories.Where(ph => original_position.Origin == ph.Previous))
            {
                AppendOreder(m_repository.PositionsLocal()[i.Position],
                decreeOperationsRequest);
                /*AppendOreder(m_repository.PositionsLocal()[i.Previous],
                decreeOperationsRequest);*/
            }
            AppendOreder(m_repository.PositionsLocal()[original_position.Origin],
                decreeOperationsRequest);
            SortByMetaDateActive(m_order);
        }

        private void AppendOreder(Structure structure, DecreeOperationsRequest decreeOperationsRequest, int multiplyer = -1)
        {
            decreeOperationsRequest.SubjectID = structure.Id * multiplyer;
            Decree local_decree;
            List<DecreeoperationManagement> for_cicle = m_repository.RequestDecreeOperations(decreeOperationsRequest, full_output_flag: true).ToList();
            foreach (DecreeoperationManagement j in for_cicle)
            {
                local_decree = m_repository.DecreesLocal()[j.Decree];
                //j.MetaDateActive = local_decree.Dateactive.GetValueOrDefault();
                j.MetaDecreeName = local_decree.Name;
                j.MetaDecreeNumber = local_decree.Number;
                j.MetaDecreeNameUnofficial = local_decree.Nickname;
                j.Changedtext = structure.Nameshortened;

                CheckAndAddByOrder(j, local_decree, this.m_order);
            }
        }

        private void AppendOreder(Position position, DecreeOperationsRequest decreeOperationsRequest, int multiplyer = 1)
        {
            decreeOperationsRequest.SubjectID = position.Id * multiplyer;
            Decree local_decree;

            foreach (DecreeoperationManagement j in m_repository.RequestDecreeOperations(decreeOperationsRequest, full_output_flag: true))
            {
                local_decree = m_repository.DecreesLocal()[j.Decree];
                j.Dateactive = j.MetaDateActive;
                j.MetaDateActive = local_decree.Dateactive.GetValueOrDefault();
                j.MetaDecreeName = local_decree.Name;
                j.MetaDecreeNumber = local_decree.Number;
                j.MetaDecreeNameUnofficial = local_decree.Nickname;
                j.Changedtext = m_repository.PositiontypesLocal()[position.Positiontype].Name;

                CheckAndAddByOrder(j, local_decree, this.m_order);
            }
        }

        private void CheckAndAddByOrder(DecreeoperationManagement decry_operation, Decree decree, List<Order> orders)
        {
            foreach(Order i in orders)
            {
                if(decree.Id == i.decree.Id)
                {
                    i.orders.Add(decry_operation);
                    return;
                }
            }
            Order time_order = new Order(decree, decry_operation);
            orders.Add(time_order);
            return;
        }

        private void SortByMetaDateActive(List<DecreeoperationManagement> items)
        {
            items.Sort((a, b) => b.MetaDateActive.Ticks.CompareTo(a.MetaDateActive.Ticks));
        }

        private void SortByMetaDateActive(List<Order> items)
        {
            items.Sort((a, b) => b.decree.Dateactive.GetValueOrDefault().Ticks.CompareTo(a.decree.Dateactive.GetValueOrDefault().Ticks));
            foreach(Order i in items)
            {
                SortByMetaDateActive(i.orders);
            }
        }

        private void ConfigureOrders()
        {
            foreach(Order i in m_order)
            {
                i.ConfigureDecreeName();
            }
        }

        private void appendnewDecree_and_decreeOperation(DecreeHistroryElementToAppending history_decree)
        {
            history_decree.history = 1;
            Decree appendingDecree = this.checkHistoryDecreeByRepository(history_decree);

            if (m_repository.DecreeoperationsLocal().Values.Where(s => s.Decree == appendingDecree.Id &&
                                                                       s.Subject == m_repository.GetOriginalStructure(history_decree.structure_id).Id * (-1)).Count() > 0)
                return;
            m_repository.AddNewDecreeOperation(new Decreeoperation() {
                Subject = -m_repository.GetOriginalStructure(history_decree.structure_id).Id,
                Changed = 1,
                Decree = appendingDecree.Id,
                Dateactive = appendingDecree.Dateactive.GetValueOrDefault(),
                Datecustom = 1
            });
        }

        private Decree checkHistoryDecreeByRepository(DecreeHistroryElementToAppending history_decree)
        {
            List<KeyValuePair<int, Decree>> time = m_repository.DecreesLocal().Where(s => s.Value.Number == history_decree.number).ToList();
            if (time == null || time.Count() == 0)
                return m_repository.AddNewDecree(history_decree, m_user);
            else
                return time[0].Value;
        }

        private void updateRepositorey_init_structure(int structure_id)
        {
            m_repository.UpdateDecreesLocal();
            m_repository.UpdateDecreeoperationsLocal();
            Initializ_structure(structure_id);
        }

        private void removeHistoryDecreeOrDecreeOperetion(Order order, Structure Original_structure)
        {
            List<DecreeoperationManagement> decreeOperationToDeleted = order.orders.Where(s => s.Decree == order.decree.Id &&
                                                                                          s.Subject == Original_structure.Id * (-1) &&
                                                                                          order.decree.Historycal == 1).ToList();
            if (decreeOperationToDeleted.Count == 0 || decreeOperationToDeleted == null)
                return;
            removeHistoryDecreeOperation(decreeOperationToDeleted);
            removeHistoryDecree(order, decreeOperationToDeleted.Count);
        }

        private void removeHistoryDecreeOperation(List<DecreeoperationManagement> decreeoperations)
        {
            pmContext time_context = m_repository.GetContext();
            foreach (DecreeoperationManagement i in decreeoperations)
            {
                Decreeoperation decree_to_remove = m_repository.DecreeoperationsLocal()[i.Id];
                time_context.Decreeoperation.Remove(decree_to_remove);
                time_context.SaveChanges();
            }
            time_context.SaveChanges();
            m_repository.UpdateDecreeoperationsLocal();
        }

        private void removeHistoryDecree(Order order, int deleted_decreeoperetions)
        {
            if (deleted_decreeoperetions == order.orders.Count)
                return;
            pmContext time_context = m_repository.GetContext();
            Decree decree_to_remove = m_repository.DecreesLocal()[order.decree.Id];
            time_context.Decree.Remove(decree_to_remove);
            time_context.SaveChanges();
            m_repository.UpdateDecreesLocal();
        }

        private void editHistoryDecreeOperation(Order old_order, DecreeHistroryElementToAppending template_new_decree)
        {
            pmContext time_context = m_repository.GetContext();
            List<DecreeoperationManagement> time = old_order.orders.Where(s => s.Subject == m_repository.GetOriginalStructure(template_new_decree.structure_id).Id * (-1)).ToList();
            foreach (DecreeoperationManagement i in time)
            {
                Decreeoperation decree_to_edit = m_repository.DecreeoperationsLocal()[i.Id];
                decree_to_edit.Dateactive = template_new_decree.date.GetValueOrDefault();
                time_context.SaveChanges();
            }
            time_context.SaveChanges();
            m_repository.UpdateDecreeoperationsLocal();

            editHistoryDecree(old_order, template_new_decree);
        }

        private void editHistoryDecree(Order old_order, DecreeHistroryElementToAppending template_new_decree)
        {
            pmContext time_context = m_repository.GetContext();
            Decree decree_to_edit = m_repository.DecreesLocal()[old_order.decree.Id];
            decree_to_edit.Dateactive = template_new_decree.date.GetValueOrDefault();
            decree_to_edit.Datesigned = template_new_decree.date.GetValueOrDefault();
            decree_to_edit.Number = template_new_decree.number;

            time_context.SaveChanges();
            m_repository.UpdateDecreeoperationsLocal();

        }
    }
}
