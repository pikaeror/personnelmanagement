using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace PersonnelManagement.Models
{
    public class DecreeOperationWorker
    {
        public DecreeOperationWorker(Repository repo)
        {
            m_repository = repo;
            position_decrees = m_repository.GetContext().Positiondecreeoperation;
            structure_decrees = m_repository.GetContext().Structuredecreeoperation;
            decree_operations = m_repository.GetContext().Decreeoperation;
            structures = m_repository.GetContext().Structure;
            positions = m_repository.GetContext().Position;
            decrees = m_repository.DecreesLocal();

        }

        public DecreeOperationWorker(ref Repository repo)
        {
            m_repository = repo;
            position_decrees = m_repository.GetContext().Positiondecreeoperation;
            structure_decrees = m_repository.GetContext().Structuredecreeoperation;
            decree_operations = m_repository.GetContext().Decreeoperation;
            structures = m_repository.GetContext().Structure;
            positions = m_repository.GetContext().Position;
            decrees = m_repository.DecreesLocal();

        }

        public Repository m_repository { get; private set; }
        public DbSet<Positiondecreeoperation> position_decrees { get; set; }
        public DbSet<Structuredecreeoperation> structure_decrees { get; set; }
        public DbSet<Decreeoperation> decree_operations { get; set; }
        public DbSet<Structure> structures { get; set; }
        public DbSet<Position> positions { get; set; }
        public Dictionary<int, Decree> decrees { get; set; }
        public USERS.User user { get; set; }

        public void worker_partial(USERS.User user)
        {
            if (user.Id != 1)
                return;
            Thread tr_p = new Thread(partial_decreeoperations_pos);
            Thread tr_s = new Thread(partial_decreeoperations_str);
            tr_p.Priority = ThreadPriority.Highest;
            tr_s.Priority = ThreadPriority.Highest;
            tr_p.Start();
            tr_s.Start();
            tr_p.Join();
            tr_s.Join();
            m_repository.SaveChanges();
        }
        public void partial_decreeoperations()
        {
            if (user.Id != 1)
                return;
            List<Decreeoperation> decree_operations = this.decree_operations.Where(r => m_repository.DecreesLocal()[r.Decree].Signed == 1).ToList();
            List<Structure> structures = this.structures.ToList();
            List<Position> positions = this.positions.ToList();
            List<Positionhistory> Positionhistory = this.m_repository.GetContext().Positionhistory.ToList();
            string pos = "", stru = "";
            foreach (Decreeoperation i in decree_operations)
            {
                if(i.Subject > 0)
                {
                    Structure time_structure;
                    try {
                        try {
                            var t = positions.Where(d => d.Id == i.Subject).First();
                        } catch {
                            pos += i.Subject.ToString() + "\n"; 
                            continue;
                        }
                        time_structure = structures.Where(r => r.Id == positions.Where(d => d.Id == i.Subject).First().Structure).First();
                    } catch {
                        stru += positions.Where(d => d.Id == i.Subject).First().Structure.ToString() + "\n";
                        continue;
                    }
                    List<Positionhistory> his = Positionhistory.Where(d => (d.Position == i.Subject && d.Decreeoperation == i.Id && d.Decree == i.Decree)).ToList();
                    if(his.Count == 0)
                    {
                        position_decrees.Add(new Positiondecreeoperation(i, ref time_structure));
                        continue;
                    }
                    var change = positions.Where(r => r.Id == his.First().Previous);
                    Position time_pos = change.First();
                    if (change.ToList().Count > 0)
                    {
                        position_decrees.Add(new Positiondecreeoperation(i, ref time_structure, ref time_pos));
                    }
                    else
                    {
                        position_decrees.Add(new Positiondecreeoperation(i, ref time_structure));
                    }
                } else if(i.Subject < 0)
                {
                    if (i.Changed == 1)
                    {
                        List<Structure> all_struct = structures.Where(r => r.Changeorigin == i.Changedtype || r.Id == i.Changedtype).ToList().OrderBy(r => r.Id).ToList();
                        List<Decreeoperation> current_decree = decree_operations.Where(r => r.Changedtype == i.Changedtype).ToList().OrderBy(r => r.Dateactive.GetValueOrDefault().Ticks).ToList();
                        int index = current_decree.FindIndex(r => r.Subject == i.Subject);
                        Structure current_struct = all_struct.Where(r => r.Id == Math.Abs(i.Subject)).First();
                        if(index != 0)
                        {
                            current_struct = all_struct.Where(r => r.Id == Math.Abs(current_decree[index - 1].Subject)).First();
                        }
                        structure_decrees.Add(new Structuredecreeoperation(i, ref current_struct));
                    }
                    else
                    {
                        structure_decrees.Add(new Structuredecreeoperation(i));
                    }
                }
                Console.WriteLine(i.Id.ToString() + "\tfrom\t" + decree_operations.Count.ToString());
            }
            System.IO.File.WriteAllText(@"d:\decree\pos.txt", pos, System.Text.Encoding.UTF8);
            System.IO.File.WriteAllText(@"d:\decree\stru.txt", stru, System.Text.Encoding.UTF8);
            m_repository.SaveChanges();
            return;
        }
        public void partial_decreeoperations_pos()
        {
            if (user.Id != 1)
                return;
            List<Decreeoperation> decree_operations = this.decree_operations.Where(r => decrees[r.Decree].Signed == 1 && r.Subject > 0).ToList();
            List<Structure> structures = this.structures.ToList();
            List<Position> positions = this.positions.ToList();
            List<Positionhistory> Positionhistory = this.m_repository.GetContext().Positionhistory.ToList();
            string pos = "", stru = "";
            IEnumerable<Positiondecreeoperation> output = new List<Positiondecreeoperation>();
            foreach (Decreeoperation i in decree_operations)
            {
                if (i.Subject > 0)
                {
                    Structure time_structure;
                    try
                    {
                        try
                        {
                            var t = positions.Where(d => d.Id == i.Subject).First();
                        }
                        catch
                        {
                            pos += i.Subject.ToString() + "\n";
                            Console.WriteLine(i.Id.ToString() + "\tfrom\t" + decree_operations.Count.ToString());
                            continue;
                        }
                        time_structure = structures.Where(r => r.Id == positions.Where(d => d.Id == i.Subject).First().Structure).First();
                    }
                    catch
                    {
                        stru += positions.Where(d => d.Id == i.Subject).First().Structure.ToString() + "\n";
                        Console.WriteLine(i.Id.ToString() + "\tfrom\t" + decree_operations.Count.ToString());
                        continue;
                    }
                    List<Positionhistory> his = Positionhistory.Where(d => (d.Position == i.Subject && d.Decreeoperation == i.Id && d.Decree == i.Decree)).ToList();
                    if (his.Count == 0)
                    {
                        output = output.Append(new Positiondecreeoperation(i, ref time_structure));
                        Console.WriteLine(i.Id.ToString() + "\tfrom\t" + decree_operations.Count.ToString());
                        continue;
                    }
                    var change = positions.Where(r => r.Id == his.First().Previous);
                    Position time_pos = change.First();
                    if (change.ToList().Count > 0)
                    {
                        output = output.Append(new Positiondecreeoperation(i, ref time_structure, ref time_pos));
                    }
                    else
                    {
                        output = output.Append(new Positiondecreeoperation(i, ref time_structure));
                    }
                }
                Console.WriteLine(i.Id.ToString() + "\tfrom\t" + decree_operations.Count.ToString());
            }
            System.IO.File.WriteAllText(@"d:\decree\pos.txt", pos, System.Text.Encoding.UTF8);
            System.IO.File.WriteAllText(@"d:\decree\stru.txt", stru, System.Text.Encoding.UTF8);
            m_repository.GetContext().Positiondecreeoperation.AddRange(output);
            m_repository.GetContext().SaveChanges();
            //m_repository.SaveChanges();
            return;
        }
        public void partial_decreeoperations_str()
        {
            if (user.Id != 1)
                return;
            List<Decreeoperation> decree_operations = this.decree_operations.Where(r => decrees[r.Decree].Signed == 1 && r.Subject < 0).ToList();
            List<Structure> structures = this.structures.ToList();
            List<Position> positions = this.positions.ToList();
            IEnumerable<Structuredecreeoperation> output = new List<Structuredecreeoperation>();
            foreach (Decreeoperation i in decree_operations)
            {
                if (i.Subject < 0)
                {
                    if (i.Changed == 1)
                    {
                        List<Structure> all_struct = structures.Where(r => r.Changeorigin == i.Changedtype || r.Id == i.Changedtype).ToList().OrderBy(r => r.Id).ToList();
                        List<Decreeoperation> current_decree = decree_operations.Where(r => r.Changedtype == i.Changedtype).ToList().OrderBy(r => r.Dateactive.GetValueOrDefault().Ticks).ToList();
                        int index = current_decree.FindIndex(r => r.Subject == i.Subject);
                        Structure current_struct = all_struct.First();
                        if (index != 0)
                        {
                            current_struct = all_struct.Where(r => r.Id == Math.Abs(current_decree[index - 1].Subject)).First();
                        }
                        output = output.Append(new Structuredecreeoperation(i, ref current_struct));
                    }
                    else
                    {
                        output = output.Append(new Structuredecreeoperation(i));
                    }
                }
                Console.WriteLine(i.Id.ToString() + "\tfrom\t" + decree_operations.Count.ToString());
            }
            /*orgContext time = m_repository.GetContext();
            foreach(Structuredecreeoperation i in output)
            {
                time.Structuredecreeoperation.Add(i);
                time.SaveChanges();
            }*/
            m_repository.GetContext().Structuredecreeoperation.AddRange(output);
            m_repository.GetContext().SaveChanges();
            return;
        }

        public List<Positiondecreeoperation> GetPositionOperationsByDecree(Decree decree, USERS.User user)
        {
            List<Positiondecreeoperation> time = position_decrees.Where(r => r.generateByDecree(ref decree, structures, positions, m_repository, user.Date.GetValueOrDefault())).ToList();
            return new List<Positiondecreeoperation>();
        }
    }
}
