using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class DecreeoperationManagement
    {

        public int Id { get; set; }
        public int Decree { get; set; }
        public int Subject { get; set; } // У подразделений subject имеет знак минуса
        public sbyte Created { get; set; }
        public sbyte Deleted { get; set; }
        public sbyte Changed { get; set; }
        public int? Changedtype { get; set; } // 1 (or undefined) - name 
        public string Changedtext { get; set; }


        public string DecreeActionDescr { get; set; }
        public string DecreeChangedTypeDescr { get; set; }
        public string DecreeConnectionTypeDescr { get; set; }
        public string DecreeSubjectNameDescr { get; set; }

        /**
         * Date when operation starts working
         */
        public DateTime MetaDateActive { get; set; }
        /**
         * If element will be deleted, date when it will become inactive
         */
        //public DateTime MetaDateInactive { get; set; }
        public string MetaDecreeName { get; set; }
        public string MetaDecreeNameUnofficial { get; set; }
        public string MetaDecreeNumber { get; set; }
        public int MetaPurposeForSubject { get; set; }  // 0 - no purpose, 1 - no purpose not signed, 
                                                        // 2 - will create subject in future, 3 - will create subject in future not signed,
                                                        // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
                                                        // 7 - will delete subject in future not signed,
                                                        // 12 - deleted, 13 - deleted not signed, 14 - renamed not signed, 15 - will be renamed,
                                                        // 16 - will be renamed not signed
        public int MetaStatus { get; set; } // 0 - nothing, 1 - delete decreeoperation including its subject

        public DateTime? Dateactive { get; set; }
        public sbyte Datecustom { get; set; }

        /**
         * Hierarchial tree. For example, if decreeoperation subject is position, then it could be like this: structurename,departmentname,positionname
         */
        public string Tree { get; set; }

        /**
         * Tree including element itself.
         */
        public string FullTree { get; set; }

        /**
         * Starts from 0. For example, position in tree "structurename,departmentname,subdepartmentname,positionname" has rank 3. 
         */
        public int TreeRank { get; set; }

        /**
         * Visual alignment of decreeoperation. Starts from zero (most left). 
         */
        public int VisualRank { get; set; }

        /**
         * Used for sorting. Array where located names of each level
         */
        public string[] SortTree { get; set; }
        /**
         * Used for sorting
         */
        public int[] SortTreeInt { get; set; }

        /**
         * Used for sorting
         */
        public long[] SortGroup { get; set; }
        /**
         * Used for sorting
         */
        public string SortCurrentElement { get; set; }
        /**
         * User for sorting
         */
        public long SortPreviousGroup { get; set; }

        /**
         * Identification of fathers to strictly find location in tree
         */
        public long ParentHash { get; set; }

        /**
         * Hash of element if he was a father
         */
        public long Hash { get; set; }

        /**
         * If compression were used, means quantity of elements we added.
         */
        public int CompressionAdded { get; set; }
        /**
         * If compression were used, means quantity of elements we deleted.
         */
        public int CompressionDeleted { get; set; }

        public void Fulfill(Decreeoperation decreeoperation)
        {
            Id = decreeoperation.Id;
            Decree = decreeoperation.Decree;
            Subject = decreeoperation.Subject;
            Created = decreeoperation.Created;
            Deleted = decreeoperation.Deleted;
            Changed = decreeoperation.Changed;
            Changedtype = decreeoperation.Changedtype;
        }
    }
}
