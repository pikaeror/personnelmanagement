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
        private Person m_person { get; set; }
        private string m_session_id { get; set; }
        private User m_user { get; set; }

        private List<DataPeriod> m_all_job_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_education_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_ill_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_work_trip_periods { get; set; } = new List<DataPeriod>();
        private List<DataPeriod> m_all_vacation_periods { get; set; } = new List<DataPeriod>();

        private CustomDate m_full_start { get; set; } = new CustomDate(0, 0, 0);
        private CustomDate m_full_end { get; set; } = new CustomDate(0, 0, 0);

        public DataAnalysis(Repository repository, Person person)
        {
            m_repository = repository;
            m_person = person;
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

        public void Worker()
        {
            return;
            getJobsPeriods();
            getEducationPeriods();
            getIllPeriods();
            getWorkTripPeriods();
            getVacationPeriods();

            List<DataPeriod> time = new List<DataPeriod>();

            time = devideperioddates(time, m_all_job_periods);
            time = devideperioddates(time, m_all_education_periods);
            time = devideperioddates(time, m_all_ill_periods);
            time = devideperioddates(time, m_all_work_trip_periods);
            time = devideperioddates(time, m_all_vacation_periods);
            time.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));

            FileStream stream = new FileStream("D:/period_tester/" + DateTime.Now.ToString("hh-mm-ss") + ".csv", FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string header = "start;end;job;education;ill;worktrip;vacation";
                writer.WriteLine(header);
                foreach(DataPeriod i in time)
                {
                    if (true)
                    {
                        //continue;
                        string k = i.start_date.ToString("dd-MM-yyyy");
                        k += ';';
                        k += i.end_date.ToString("dd-MM-yyyy");
                        k += ';';
                        k += i.job == null ? "" : true.ToString();
                        k += ';';
                        k += i.education == null ? "" : true.ToString();
                        k += ';';
                        k += i.ill == null ? "" : true.ToString();
                        k += ';';
                        k += i.worktrip == null ? "" : true.ToString();
                        k += ';';
                        k += i.vacation == null ? "" : true.ToString();
                        writer.WriteLine(k);
                    }
                }
            }
            stream.Close();

            List<DataPeriod> for_calc = devideperioddateseducation(devideperioddatesjob(m_all_job_periods), m_all_education_periods);
            var t = devideperioddatesjob(m_all_job_periods);
            this.fullcalc(for_calc);
            var full = CustomDate.difference(m_full_end, m_full_start);
            full.rebase();
            stream = new FileStream("D:/period_tester/" + DateTime.Now.ToString("hh-mm-ss") + "filter.csv", FileMode.OpenOrCreate);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string header = "start;end;job;education;ill;worktrip;vacation";
                writer.WriteLine(header);
                foreach (DataPeriod i in for_calc)
                {
                    if (true)
                    {
                        //continue;
                        string k = i.start_date.ToString("dd-MM-yyyy");
                        k += ';';
                        k += i.end_date.ToString("dd-MM-yyyy");
                        k += ';';
                        k += i.job == null ? "" : true.ToString();
                        k += ';';
                        k += i.education == null ? "" : true.ToString();
                        k += ';';
                        k += i.ill == null ? "" : true.ToString();
                        k += ';';
                        k += i.worktrip == null ? "" : true.ToString();
                        k += ';';
                        k += i.vacation == null ? "" : true.ToString();
                        writer.WriteLine(k);
                    }
                }
            }
            stream.Close();
        }

        private void getJobsPeriods()
        {
            IEnumerable<Personjob> all_job_periods = m_repository.PersonjobsLocal().Values.Where(s => s.Person == m_person.Id);
            foreach(Personjob i in all_job_periods)
            {
                if (i.Jobtype != 2)
                    continue;
                bool flag = false;
                //DataPeriod time = new DataPeriod(i.Start.GetValueOrDefault(), i.End);
                foreach(Personjobprivelege j in m_repository.GetContext().Personjobprivelege.Select(s => s).Where(s => s.Personjob == i.Id))
                {
                    foreach(Personjobprivelegeperiod k in m_repository.GetContext().Personjobprivelegeperiod.Select(s => s).Where(s => s.Personjobprivelege == j.Id))
                    {
                        if(flag)
                            m_all_job_periods.Add(new DataPeriod(m_all_job_periods.Last().end_date.AddDays(1), k.Start.GetValueOrDefault().AddDays(-1), job: i));
                        else
                        {
                            m_all_job_periods.Add(new DataPeriod(i.Start.GetValueOrDefault(), k.Start.GetValueOrDefault().AddDays(-1), job: i));
                            flag = true;
                        }
                        m_all_job_periods.Add(new DataPeriod(k.Start.GetValueOrDefault(), k.End.GetValueOrDefault(), j.Coef, job: i));
                    }
                }
                if(!flag)
                    m_all_job_periods.Add(new DataPeriod(i.Start.GetValueOrDefault(), end_date: i.Actual != 1 ? i.End.GetValueOrDefault() : DateTime.Now, job: i));
                else
                    m_all_job_periods.Add(new DataPeriod(m_all_job_periods.Last().end_date.AddDays(1), end_date: i.Actual != 1 ? i.End.GetValueOrDefault() : DateTime.Now, job: i));
            }
            m_all_job_periods.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
        }

        private void getEducationPeriods()
        {
            foreach (Personeducation i in m_repository.GetContext().Personeducation.Select(s => s).Where(s => s.Person == m_person.Id))
            {
                DataPeriod time = new DataPeriod(i.Start.GetValueOrDefault(), i.End);
                foreach (Educationtypeblock j in m_repository.GetContext().Educationtypeblock.Select(s => s).Where(s => s.Personeducation == i.Id))
                {
                    foreach (Educationperiod k in m_repository.GetContext().Educationperiod.Select(s => s).Where(s => s.Educationtypeblock == j.Id))
                    {
                        m_all_education_periods.Add(new DataPeriod(k.Start.GetValueOrDefault(), k.End.GetValueOrDefault(), education: i));
                    }
                }
            }
            m_all_education_periods.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
        }

        private void getIllPeriods()
        {
            foreach (Personill i in m_repository.GetContext().Personill.Select(s => s).Where(s => s.Person == m_person.Id))
            {
                if (i.Privelege == 0)
                    m_all_ill_periods.Add(new DataPeriod(i.Datestart,
                        i.Dateend,
                        ill: i));
            }
            m_all_ill_periods.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
        }

        private void getWorkTripPeriods()
        {
            foreach (Personworktrip i in m_repository.GetContext().Personworktrip.Select(s => s).Where(s => s.Person == m_person.Id))
            {
                if (i.Privelege == 0)
                    m_all_work_trip_periods.Add(new DataPeriod(i.Tripdate,
                    i.Days,
                    worktrip: i));
            }
            m_all_work_trip_periods.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
        }

        private void getVacationPeriods()
        {
            foreach (Personvacation i in m_repository.GetContext().Personvacation.Select(s => s).Where(s => s.Person == m_person.Id))
            {
                m_all_vacation_periods.Add(new DataPeriod(i.Date.GetValueOrDefault(),
                    i.Duration,
                    vacation: i));
            }
            m_all_vacation_periods.Sort((a, b) => a.start_date.Ticks.CompareTo(b.start_date.Ticks));
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
                            j.multiply_coef,
                            job: j.job,
                            education: j.education,
                            ill: j.ill,
                            worktrip: j.worktrip,
                            vacation: j.vacation));
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
                                i.multiply_coef,
                                job: i.job,
                                education: i.education,
                                ill: i.ill,
                                worktrip: i.worktrip,
                                vacation: i.vacation));
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
                        i.end_date == j.end_date &&
                        (i.education.Cadet != 1 ||
                        i.education.Educationtype != 1))
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
                                i.multiply_coef,
                                job: i.job,
                                education: i.education,
                                ill: i.ill,
                                worktrip: i.worktrip,
                                vacation: i.vacation));
                            }
                            new_general_period.Add(new DataPeriod(i.start_date,
                                j.start_date.AddDays(-1),
                                i.multiply_coef,
                                job: i.job,
                                education: i.education,
                                ill: i.ill,
                                worktrip: i.worktrip,
                                vacation: i.vacation));
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
                                i.multiply_coef,
                                job: i.job,
                                education: i.education,
                                ill: i.ill,
                                worktrip: i.worktrip,
                                vacation: i.vacation));
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
                        i.end_date == j.end_date &&
                        (i.ill.Privelege != 1 ||
                        i.ill.Privelege != 1))
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
                                i.multiply_coef,
                                job: i.job,
                                education: i.education,
                                ill: i.ill,
                                worktrip: i.worktrip,
                                vacation: i.vacation));
                            }
                            new_general_period.Add(new DataPeriod(i.start_date,
                                j.start_date.AddDays(-1),
                                i.multiply_coef,
                                job: i.job,
                                education: i.education,
                                ill: i.ill,
                                worktrip: i.worktrip,
                                vacation: i.vacation));
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
                                i.multiply_coef,
                                job: i.job,
                                education: i.education,
                                ill: i.ill,
                                worktrip: i.worktrip,
                                vacation: i.vacation));
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

