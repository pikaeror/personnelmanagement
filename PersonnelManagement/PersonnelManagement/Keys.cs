using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement
{
    public static class Keys
    {
        public static readonly string COOKIES_SESSION = "sessionid";

        public const string IDENTITY_LOGIN_KEY = "ln";
        public const string IDENTITY_LOGINED_KEY = "logined";
        public const string IDENTITY_STRUCTURE_KEY = "structure";
        public const string IDENTITY_ADMIN_KEY = "admin";
        public const string IDENTITY_MASTERPERSONNELEDITOR_KEY = "mpe";
        public const string IDENTITY_STRUCTUREEDITOR_KEY = "se";
        public const string IDENTITY_STRUCTUREREAD_KEY = "sr";
        public const string IDENTITY_DECREE_KEY = "decree";
        public const string IDENTITY_DECREE_NAME_KEY = "decreename";
        public const string IDENTITY_PERSONNELEDITOR_KEY = "pe";
        public const string IDENTITY_PERSONNELREAD_KEY = "pr";
        public const string IDENTITY_MODE_KEY = "mode";
        public const string IDENTITY_POSITIONCOMPACT_KEY = "positioncompact";
        public const string IDENTITY_DATE_KEY = "date";
        public const string IDENTITY_SIDEBAR_DISPLAY_KEY = "sidebardisplay";
        public const string IDENTITY_CURRENTSTRUCTURETREE_KEY = "cst";
        public const string IDENTITY_FULLMODE_KEY = "fullmode";

        public static readonly string IDENTITY_LOGINED_TRUE = "logtr";
        public static readonly string IDENTITY_LOGINED_FALSE = "logfa";
        public static readonly string IDENTITY_SESSIONID_PREFIX = "sessionid";

        public const string STATUS_DELETE = "DELETE";
        public const string STATUS_NULLIFYPASS = "NULLIFYPASS";
        
        public const string SUCCESS_SHORT = "S";
        public const string ERROR_SHORT = "E";

        public const string STRUCTURE_MANAGEMENT_ADDNEWSTRUCTURE = "addnewstructure";
        public const string STRUCTURE_MANAGEMENT_REMOVESTRUCTURE = "removestructure";
        public const string STRUCTURE_MANAGEMENT_RENAMESTRUCTURE = "renamestructure";
        public const string STRUCTURE_MANAGEMENT_RENAMESTRUCTURENODECREE = "renamestructurenodecree";
        public const string STRUCTURE_MANAGEMENT_REMOVESTRUCTUREDECREE = "removestructuredecree";
        public const string STRUCTURE_MANAGEMENT_RENAMESTRUCTUREDECREE = "renamestructuredecree";

        public const string DEPARTMENT_MANAGEMENT_ADDNEWDEPARTMENT = "addnewdepartment";
        public const string DEPARTMENT_MANAGEMENT_REMOVEDEPARTMENT = "removedepartment";
        public const string DEPARTMENT_MANAGEMENT_RENAMEDEPARTMENT = "renamedepartment";

        public const int DECREE_MANAGEMENT_NEWDECREE = 1;
        public const int DECREE_MANAGEMENT_DECLINEDECREE = 2;
        public const int DECREE_MANAGEMENT_ACCEPTDECREE = 3;
        public const int DECREE_MANAGEMENT_PRINTDECREE = 4;
        public const int DECREE_MANAGEMENT_UPDATEDECREEINFO = 5; // 5 - save decree info (date) changes.
        public const int DECREE_MANAGEMENT_FILTERSIGNEDECREE = 6; // 5 - save decree info (date) changes.

        public const int PERSONDECREE_MANAGEMENT_NEWDECREE = 1; // 1 - Создать новый проект приказа
        public const int PERSONDECREE_MANAGEMENT_DECLINEDECREE = 2; // 2 - отменить проект приказа
        public const int PERSONDECREE_MANAGEMENT_ACCEPTDECREE = 3; // 3 - подписать проект приказа
        public const int PERSONDECREE_MANAGEMENT_PRINTDECREE = 4; // 4 - распечатать приказ
        public const int PERSONDECREE_MANAGEMENT_UPDATEDECREEINFO = 5; // 5 - обновить информацию проекта приказа (номер, дата)
        public const int PERSONDECREE_MANAGEMENT_LOCKDECREE = 6; // 6 - предоставить к общему доступу/закрыть общий доступ.
        public const int PERSONDECREE_MANAGEMENT_CHANGEOWNER = 7; // 7 - направить проект приказа другому пользователю.

        public const int STAFF_MANAGEMENT_TYPE_ALL = 0; // signed and unsigned.

        public const int DECREE_OPERATION_CONNECTION_POSITION = 1;
        public const int DECREE_OPERATION_CONNECTION_DEPARTMENT = 2;
        public const int DECREE_OPERATION_CONNECTION_STRUCTURE = 3;
        public const int DECREE_OPERATION_CHANGED_TYPE_NAME = 1; // 1 (or undefined) - name 
        public const int DECREE_OPERATION_META_REMOVE = 1; // 1 - delete decreeoperation including its subject

        // 0 - no purpose, 1 - no purpose not signed, 
        // 2 - will create subject in future, 3 - will create subject in future not signed,
        // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
        // 7 - will delete subject in future not signed,
        // 12 - deleted, 13 - deleted not signed, 14 - renamed not signed, 15 - will be renamed,
        // 16 - will be renamed not signed
        public const int DECREE_OPERATION_META_PURPOSE_NO_PURPOSE = 0;
        public const int DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED = 1;
        public const int DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE = 2;
        public const int DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE_NOT_SIGNED = 3;
        public const int DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT = 4;
        public const int DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT_NOT_SIGNED = 5;
        public const int DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE = 6;
        public const int DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE_NOT_SIGNED = 7;
        public const int DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT = 12;
        public const int DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT_NOT_SIGNED = 13;
        public const int DECREE_OPERATION_META_PURPOSE_RENAMED_SUBJECT_NOT_SIGNED = 14;
        public const int DECREE_OPERATION_META_PURPOSE_RENAMED_SUBJECT_IN_FUTURE = 15;
        public const int DECREE_OPERATION_META_PURPOSE_RENAMED_SUBJECT_IN_FUTURE_NOT_SIGNED = 16;

        // 12 - deleted, 13 - deleted not signed, 14 - renamed not signed, 15 - will be renamed,
        // 16 - will be renamed not signed

        public const string POSITION_MANAGEMENT_ADDNEWPOSITION = "addnewposition";
        public const string POSITION_MANAGEMENT_REMOVEPOSITION = "removeposition";
        public const string POSITION_MANAGEMENT_RENAMEPOSITION = "renameposition";
        public const string POSITION_MANAGEMENT_REMOVEPOSITIONDECREE = "removepositiondecree";
        public const string POSITION_MANAGEMENT_RENAMEPOSITIONDECREE = "renamepositiondecree";
        public const string POSITION_MANAGEMENT_ADDNEWDEPARTMENT = "addnewdepartment";
        public const string POSITION_MANAGEMENT_REMOVEDEPARTMENT = "removedepartment";
        public const string POSITION_MANAGEMENT_RENAMEDEPARTMENT = "renamedepartment";
        public const string POSITION_MANAGEMENT_UPDATEPOSITIONSBYPOSITIONTYPE = "updatepositionsbypositiontype";
        public const int POSITION_PARENT_TYPE_DEPARTMENT_0 = 0;
        public const int POSITION_PARENT_TYPE_DEPARTMENT_1 = 1;
        public const int POSITION_PARENT_TYPE_STRUCTURE = 2;

        public const string TREE_BEAUTY = " — ";
        public const char TREE_SEPARATOR = '&';

        public const int PMREQUEST_TYPE_POSITION = 1;
        public const int PMREQUEST_TYPE_STRUCTURE = 2;
        public const int PMREQUEST_TYPE_LOAD = 5;
        public const int PMREQUEST_TYPE_ADDREMOVE = 6;

        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
        {
            TV value;
            if (!dict.ContainsKey(key)) { return defaultValue; }
            return dict.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}
