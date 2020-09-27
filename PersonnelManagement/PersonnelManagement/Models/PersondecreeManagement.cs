using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PersondecreeManagement: Persondecree
    {


        public int PersondecreeManagementStatus { get; set; } // 1 - Создать новый проект приказа, 2 - отменить проект приказа, 3 - подписать проект приказа, 4 - распечатать приказ
                                                              // 5 - обновить информацию проекта приказа (номер, дата), 6 - предоставить к общему доступу/закрыть общий доступ.
                                                              // 7 - направить проект приказа другому пользователю.

        public UserManager CreatorObject { get; set; }
        public PersondecreeManagement()
        {
            
        }

        public PersondecreeManagement(Persondecree persondecree)
        {
            Id = persondecree.Id;
            Datecreated = persondecree.Datecreated;
            Datesigned = persondecree.Datesigned;
            Creator = persondecree.Creator;
            Owner = persondecree.Owner;
            Name = persondecree.Name;
            Nickname = persondecree.Nickname;
            Number = persondecree.Number;
            Numbertype = persondecree.Numbertype;
            Transfer = persondecree.Transfer;
            Signed = persondecree.Signed;

            CreatorObject = null;
        }


        
    }
}
