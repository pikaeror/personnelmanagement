import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button, Dropdown, Dialog, DropdownItem, DropdownMenu, Popover, Checkbox, Tooltip, Upload, Autocomplete, Table, TableColumn } from 'element-ui';
import _ from 'lodash';
import '../../css/print.css';
import Rewardmoney from '../../classes/rewardmoney'
import Persondecreeoperation from '../../classes/persondecreeoperation'
import Persondecreeblock from '../../classes/persondecreeblock'
import Persondecreeblocktype from '../../classes/persondecreeblocktype'
import Persondecreeblocksub from '../../classes/persondecreeblocksub'
import Persondecreeblocksubtype from '../../classes/persondecreeblocksubtype'
import User from '../../classes/user';
import Persondecree from '../../classes/persondecree';

Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Dropdown.name, Dropdown);
Vue.component(DropdownItem.name, DropdownItem);
Vue.component(DropdownMenu.name, DropdownMenu);
Vue.component(Popover.name, Popover);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Tooltip.name, Tooltip);
Vue.component(Dialog.name, Dialog);
Vue.component(Upload.name, Upload);
Vue.component(Autocomplete.name, Autocomplete);
Vue.use(Element);
Vue.use(Table)
Vue.use(TableColumn)

@Component({
    components: {
        //decreeoperationtemplatecreator: require('../decreeoperationtemplatecreator/decreeoperationtemplatecreator.vue.html')
    }
})
export default class derceeoperationelement extends Vue {

    @Prop({ default: 0 })
    visible: number;

    menuid: number;
    update: boolean;

    persondecreeOperations: Persondecreeoperation[];
    persondecreeId: number;
    persondecreeNumber: string;
    persondecreeNumbertype: string;
    persondecreeNickname: string;
    persondecreeName: string;
    persondecreeDatecreated: string;
    persondecreeDatesigned: string;
    persondecreeCreatorObject: User;
    persondecreeBlocks: Persondecreeblock[];
    persondecreesNewblock: number;
    persondecreeBlocksubs: Persondecreeblocksub[];
    persondecreesNewblocksub: number;
    persondecreesActionmenu: boolean;

    datefiltervariants: string;

    fullpersondecrees: Persondecree[];
    viewpersondecrees: Persondecree[];

    multipleSelection: Persondecree[];

    decreecreator: boolean;

    dialogVisibleSend: boolean;
    dialogVisibleUnit: boolean;
    dialogVisibleExplorer: boolean;
    explorerFolder: ["Мои документы", "Входящие", "Исходящие", "Отработанные", "Архив", "В работе"];

    usersSearch: User[];
    userSelected: User;
    usersearch: string;

    data() {
        return {
            menuid: 0,
            update: true,

            persondecreeOperations: [],
            persondecreeId: 0,
            persondecreeNumber: "",
            persondecreeNumbertype: "",
            persondecreeNickname: "",
            persondecreeName: "",
            persondecreeDatecreated: "",
            persondecreeDatesigned: "",
            persondecreeCreatorObject: null,
            persondecreeBlocks: [],
            persondecreesNewblock: null,
            persondecreeBlocksubs: [],
            persondecreesNewblocksub: null,
            persondecreesActionmenu: false,

            datefiltervariants: "",

            fullpersondecrees: [],
            viewpersondecrees: [],

            multipleSelection: [],

            decreecreator: false,

            dialogVisibleSend: false,
            dialogVisibleUnit: false,
            dialogVisibleExplorer: false,
            explorerFolder: ["Мои документы", "Входящие", "Исходящие", "Отработанные", "Архив", "В работе"],

            usersSearch: [],
            userSelected: null,
            usersearch: "",
        }
    }

    tableRowClassName({ row, rowIndex }) {
        if (row.signed == 1) {
            return 'success-row';
        }
        return '';
    }
    handleSelectionChange(val) {
        this.multipleSelection = val;
        this.multipleSelection.forEach(r => {
            let index: number = this.fullpersondecrees.findIndex(p => p.id == r.id);
            this.fullpersondecrees[index].marked = this.fullpersondecrees[index].marked == true ? false : true;
        })
        /*if (val) {
            val.forEach(row => {
                this.$refs.multipleTable.toggleRowSelection(row);
            });
        } else {
            this.$refs.multipleTable.clearSelection();
        }*/
        //this.reload();
    }
    filterHandler(value, row, column) {
        const property = column['property'];
        return row[property] === value;
    }
    getDateList() {
        let outputlist = new Map();
        this.viewpersondecrees.forEach(r => {
            outputlist.set(r.getDate, r.getDate);
        });
        this.datefiltervariants = "[";
        outputlist.forEach(r => {
            this.datefiltervariants += "{ text: '" + r + "', value: '" + r + "' }, ";
        })
        this.datefiltervariants = this.datefiltervariants.slice(0, -2);
        this.datefiltervariants += "]";
        //this.datefiltervariants.toString
        return outputlist;
    }

    mounted() {
        if (this.$store.state.eldSelectedperson > 0) {
            this.$store.commit("setEldSelectedperson", 0);
        }

        //setInterval(this.fetchPersondecreesActive, 20000);
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value) {
            this.fetchPersondecreesActive();
            this.setMenuid(2);
        }
    }

    fetchPersondecreesActive() {
        let time_value = this.multipleSelection;
        fetch('api/Persondecree/FullByUser', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Persondecree[]>;
            })
            .then(result => {
                //alert('mark');
                result.forEach(r => {
                    if (this.fullpersondecrees != null) {
                        let preloadedPersondecree: Persondecree = this.fullpersondecrees.find(p => p.id == r.id);
                        if (preloadedPersondecree != null) {
                            r.marked = preloadedPersondecree.marked;
                        } else {
                            r.marked = false;
                        }
                        //r.marked = p
                    } else {
                        r.marked = false;
                    }
                    if (r.creatorObject != null) {
                        r.getFIO = ((r.creatorObject.surname == "" || r.creatorObject.surname == null) ? '' : (r.creatorObject.surname + ' ')) +
                            ((r.creatorObject.name == "" || r.creatorObject.name == null) ? '' : (r.creatorObject.name[0].toUpperCase() + '.')) +
                            ((r.creatorObject.firstname == "" || r.creatorObject.firstname == null) ? '' : (r.creatorObject.firstname[0].toUpperCase() + '.'));
                        r.getPlace = r.creatorObject.structureString;
                    }
                    r.getDate = (r.datesigned == null ? (r.datecreated == null ? '' : r.datecreated.toString().split('T')[0].split('-').reverse().join('-')) :
                        r.datesigned.toString().split('T')[0].split('-').reverse().join('-'))
                    r.getName = r.name +
                        (((r.name == null || r.name == "") || (r.nickname == null || r.nickname == "")) ? '' : ' / ') +
                        r.nickname;
                    r.getNumber = r.number.toString() + (r.numbertype == "" ? '' : (' ' + r.numbertype.toUpperCase()));
                });
                this.fullpersondecrees = result;
                this.reload();
                this.multipleSelection = time_value;
            });
        return this.fullpersondecrees;
    }

    persondecreeCreate() {
        //this.disableDecrees();
        fetch('/api/Persondecree', {
            method: 'post',
            body: JSON.stringify(<Persondecree>{
                persondecreeManagementStatus: 1, // Добавить
                nickname: "",
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => {
            (<any>Vue).notify("S:Проект приказа создан");
        })
    }

    createMenuToggle() {
        this.fullpersondecrees = null;
        //this.persondecreeCreate();

        fetch('/api/Persondecree', {
            method: 'post',
            body: JSON.stringify(<Persondecree>{
                persondecreeManagementStatus: 1, // Добавить
                nickname: "",
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => {
            (<any>Vue).notify("S:Проект приказа создан");
            fetch('api/Persondecree/GetLustDecreeByUser', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Persondecree>;
                })
                .then(result => {
                    this.open(result);
                });
        })
        /*fetch('api/Persondecree/GetLustDecreeByUser', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Persondecree>;
            })
            .then(result => {
                this.$store.commit("setpersondecree", result);
                this.$store.commit("setdecreeoperationtemplatecreatorVisible", this.$store.state.decreeoperationtemplatecreatorVisible ? false : true);
            });*/
    }

    open(decree: Persondecree) {
        this.$store.commit("setpersondecree", decree);
        this.$store.commit("setdecreeoperationtemplatecreatorVisible", this.$store.state.decreeoperationtemplatecreatorVisible ? false : true);
    }

    getMaxID(array_culc): number {
        let index: number = -1;
        array_culc.forEach(r => {
            if (r.id > index)
                index = r.id;
        })
        return index;
    }

    decreecreatorfunction(): boolean {
        return this.$store.state.derceeoperationelementcreatorVisible;
    }
    close() {
        this.$store.commit("setdecreeoperationtemplatecreatorVisible", false);
    }

    reload() {
        this.setMenuid(this.menuid);
        this.getDateList();
        this.update = false;
        this.update = true;
        /*this.getDateList();*/
    }

    setMenuid(id: number) {
        this.menuid = id;
        this.filterbyfolders(id);
        this.getDateList();
    }

    filterbyfolders(folder: number) {
        this.viewpersondecrees = [];
        if (folder == 2) {
            this.fullpersondecrees.forEach(r => {
                if (r.signed == 0 && r.creator == r.owner && r.creator == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)
            })
        } else if (folder == 3) {
            this.fullpersondecrees.forEach(r => {
                if (r.signed == 0 && r.creator != r.owner && r.owner == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)
            })
        } else if (folder == 4) {
            this.fullpersondecrees.forEach(r => {
                if (r.signed == 0 && r.creator != r.owner && r.creator == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)
            })
        } else if (folder == 8) {
            this.fullpersondecrees.forEach(r => {
                if (r.signed == 1 && r.creator == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)
            })
        } else if (folder == 9) {
            this.fullpersondecrees.forEach(r => {
                if (r.signed == 1)
                    this.viewpersondecrees.push(r)
            })
        }
    }

    rowClicked(row) {
        this.open(row);
    }

    canSelectRow(row) {
        this.viewpersondecrees.forEach(r => {
            if (r == row) {
                r.marked = !r.marked;
            }
        })
    }

    send() {
        this.dialogVisibleSend = true;
    }

    deleted() {

    }

    explorer() {
        this.dialogVisibleExplorer = true;
    }
    FolderSelected(folder) {

        this.multipleSelection = [];
        this.dialogVisibleExplorer = false;
    }
    deleteDecreeFromList(decrre) {
        let new_listdecree: Persondecree[] = [];
        this.multipleSelection.forEach(r => {
            if (r.id != decrre.id) {
                new_listdecree.push(r);
            }
        })
        this.multipleSelection = new_listdecree;
    }

    unit() {
        this.dialogVisibleUnit = true;
    }

    searchUsers(search: string) {
        fetch('api/Users/Search' + search, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<User[]>;
            })
            .then(result => {
                result.forEach(r => {
                    if (r.positionString == null) {
                        r.positionString = "";
                    }
                })
                this.usersSearch = result;
                this.userSelected = null;
            })
    }

    hasUserSearchResults(): boolean {
        if (this.usersSearch != null && this.usersSearch.length > 0) {
            return true;
        } else {
            return false;
        }
    }

    selectUser(id: number) {
        this.multipleSelection.forEach(q => {
        if (q == null) {
            return; // Нету проекта приказа
        }
        //this.disableDecrees();
        fetch('/api/Persondecree', {
            method: 'post',
            body: JSON.stringify(<Persondecree>{
                id: q.id,
                persondecreeManagementStatus: 7, // Перенаправляем проект приказа на другого кадровика
                owner: id,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => {
            this.persondecreeId = 0;
            this.fetchPersondecreesActive();
            this.usersSearch = [];
            (<any>Vue).notify("S:Проект приказа направлен на кадровика");
        })
        })
    }

    closeUserSearch() {
        this.usersSearch = [];
    }
}