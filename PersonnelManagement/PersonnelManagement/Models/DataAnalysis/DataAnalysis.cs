using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace PersonnelManagement.Models
{
    public class DataAnalysis
    {
        private Repository m_repository { get; set; }
        private string m_session_id { get; set; }
        private User m_user { get; set; }

        private List<DataPeriod> m_all_job_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_education_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_ill_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_work_trip_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_vacation_periods { get; set; } = new List<DataPeriod>();

        private CustomDate m_full_start { get; set; } = new CustomDate(0, 0, 0);
        private CustomDate m_full_end { get; set; } = new CustomDate(0, 0, 0);

        public DataAnalysis(Repository repository)
        {
            m_repository = repository;
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

        private List<DataPeriod> devideperioddates(List<DataPeriod> general_periods, List<DataPeriod> devider_period)
        {
            List<DataPeriod> new_general = new List<DataPeriod>();
            foreach (DataPeriod i in devider_period)
            {
                bool flag = true;
                foreach (DataPeriod j in general_periods)
                {
                    if(i.start_date >= j.start_date &&
                        i.start_date < j.end_date)
                    {
                        foreach(DataPeriod k in general_periods)
                        {
                            if(i.end_date >= k.start_date &&
                                i.end_date < k.end_date)
                            {
                                if (k.start_date == j.start_date &&
                                    k.end_date == j.end_date)
                                    break;
                                else
                                {
                                    k.start_date = i.end_date.AddDays(1);
                                    break;
                                }
                            }
                        }
                        general_periods.Add(new DataPeriod(j.start_date,
                            i.start_date.AddDays(-1),
                            j.multiply_coef));
                        general_periods.Add(i);
                        if (i.end_date < j.end_date)
                            j.start_date = i.end_date.AddDays(1);
                        flag = false;
                        break;
                        //general_periods.Remove(j);
                    }
                }
                if (flag)
                    general_periods.Add(i);
            }
            new_general = general_periods;
            new_general.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
            return new_general;
        }

        private List<DataPeriod> devideperioddatesjob(List<DataPeriod> general_periods)
        {
            List<DataPeriod> new_general_period = general_periods;
            foreach(DataPeriod i in new_general_period)
            {
                foreach(DataPeriod j in new_general_period)
                {
                    if (i.start_date == j.start_date &&
                        i.end_date == j.end_date)
                        continue;
                    if (j.start_date < i.end_date &&
                        j.start_date > i.start_date)
                    {
                        if (j.multiply_coef > i.multiply_coef)
                        {
                            if(j.end_date < i.end_date)
                                new_general_period.Add(new DataPeriod(j.end_date.AddDays(1),
                                i.end_date,
                                i.multiply_coef));
                            i.end_date = j.start_date.AddDays(-1);
                        } else
                        {
                            if (j.end_date < i.end_date)
                                j.multiply_coef = 0;
                            else
                                j.start_date = i.end_date.AddDays(1);
                        }
                    }
                }
            }
            new_general_period.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
            return new_general_period;
        }

        private List<DataPeriod> devideperioddateseducation(List<DataPeriod> general_periods, List<DataPeriod> education)
        {
            List<DataPeriod> new_general_period = general_periods;
            foreach (DataPeriod i in education)
            {
                bool flag = true;
                foreach (DataPeriod j in new_general_period)
                {
                    if (i.start_date == j.start_date &&
                        i.end_date == j.end_date)
                        continue;

                    if (j.start_date < i.end_date &&
                        j.start_date > i.start_date)
                    {
                        if (j.multiply_coef > i.multiply_coef)
                        {
                            if (j.end_date < i.end_date)
                            {
                                new_general_period.Add(new DataPeriod(j.end_date.AddDays(1),
                                i.end_date,
                                i.multiply_coef));
                            }
                            new_general_period.Add(new DataPeriod(i.start_date,
                                j.start_date.AddDays(-1),
                                i.multiply_coef));
                            flag = false;
                            //i.end_date = j.start_date.AddDays(-1);
                        }
                        else
                        {
                            if (j.end_date < i.end_date)
                                j.multiply_coef = 0;
                            else
                            {
                                i.end_date = j.start_date.AddDays(-1);
                                new_general_period.Add(new DataPeriod(i.end_date.AddDays(1),
                                i.end_date,
                                i.multiply_coef));
                                flag = false;
                            }
                        }
                    }
                }
                if (flag)
                {
                    new_general_period.Add(i);
                }
            }
            new_general_period.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
            return new_general_period;
        }

        private List<DataPeriod> devideperioddateseill(List<DataPeriod> general_periods, List<DataPeriod> ill)
        {
            List<DataPeriod> new_general_period = general_periods;
            foreach (DataPeriod i in ill)
            {
                bool flag = false;
                foreach (DataPeriod j in new_general_period)
                {
                    if (i.start_date == j.start_date &&
                        i.end_date == j.end_date)
                        continue;

                    if (j.start_date < i.end_date &&
                        j.start_date > i.start_date)
                    {
                        if (j.multiply_coef > i.multiply_coef)
                        {
                            if (j.end_date < i.end_date)
                            {
                                new_general_period.Add(new DataPeriod(j.end_date.AddDays(1),
                                i.end_date,
                                i.multiply_coef));
                            }
                            new_general_period.Add(new DataPeriod(i.start_date,
                                j.start_date.AddDays(-1),
                                i.multiply_coef));
                            //i.end_date = j.start_date.AddDays(-1);
                        }
                        else
                        {
                            if (j.end_date < i.end_date)
                                j.multiply_coef = 0;
                            else
                            {
                                i.end_date = j.start_date.AddDays(-1);
                                new_general_period.Add(new DataPeriod(i.end_date.AddDays(1),
                                i.end_date,
                                i.multiply_coef));
                            }
                        }
                    }
                }
                if (flag)
                {
                    new_general_period.Add(i);
                }
            }
            new_general_period.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
            return new_general_period;
        }

        private void fullcalc(List<DataPeriod> list)
        {
            foreach(DataPeriod i in list)
            {
                //i.equalCalculationEndDate();
                m_full_start.add_whithout(i.start_date);
                m_full_end.add_whithout(CustomDate.add(new CustomDate(i.start_date), i.period));
            }
        }
    }
}

