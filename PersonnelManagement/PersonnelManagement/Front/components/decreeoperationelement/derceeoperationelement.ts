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
import Mailexplorer from '../../classes/Mail/mailexplorer';
import Structure from '../../classes/Structure';

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

    full_explorers: Mailexplorer[];
    fullpersondecrees: Persondecree[];
    viewpersondecrees: Persondecree[];

    multipleSelection: Persondecree[];

    decreecreator: boolean;

    dialogVisibleSend: boolean;
    dialogVisibleUnit: boolean;
    viewpersondecreesunit: Persondecree[];
    menuunitid: number;
    dialogVisibleExplorer: boolean;
    explorerFolder: ["Мои документы", "Входящие", "Исходящие", "Отработанные", "Архив", "В работе"];
    unit_dialog: boolean;
    privios_lens: number;
    unit_decree: Persondecree = new Persondecree();

    usersSearch: User[];
    userSelected: User;
    usersearch: string;

    rowcontextmenuVisible: boolean;
    rand_list: Structure[];
    previos: Persondecree;

    excertmenu: boolean;
    excertsdecree: Persondecree;
    excertsdecreestructure: Structure[];
    title_text_excert: string;
    title_text_excert_template: string;

    upload: number;

    data() {
        return {
            menuid: 2,
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

            full_explorers: [],
            fullpersondecrees: [],
            viewpersondecrees: [],

            multipleSelection: [],

            decreecreator: false,

            dialogVisibleSend: false,
            dialogVisibleUnit: false,
            viewpersondecreesunit: [],
            menuunitid: 0,
            dialogVisibleExplorer: false,
            explorerFolder: ["Мои документы", "Входящие", "Исходящие", "Отработанные", "Архив", "В работе"],
            unit_dialog: true,
            privios_lens: 0,
            unit_decree: null,

            usersSearch: [],
            userSelected: null,
            usersearch: "",

            rowcontextmenuVisible: false,

            rand_list: [],
            previos: null,

            title_text_f: 'Направить:',
            title_text_s: 'Переместить:',
            title_text_t: 'Объединить:',
            title_text_excert: '',
            title_text_excert_template: 'Выписки из приказа:',

            excertmenu: this.$store.state.excertmenu,
            excertsdecree: null,
            excertsdecreestructure: [],

            upload: 0,
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
        /*this.multipleSelection.forEach(r => {
            let index: number = this.fullpersondecrees.findIndex(p => p.id == r.id);
            this.fullpersondecrees[index].marked = this.fullpersondecrees[index].marked == true ? false : true;
        })*/
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
    rowContextmenu(row, column, event) {
        this.rowcontextmenuVisible = false;
        this.rowcontextmenuVisible = true;
       /* 
        event.preventDefault();
        this.$nextTick(() => {
            this.$refs.contextbutton.init(row, column, event);
        })*/
    }
    foo() {
        this.rowcontextmenuVisible = false;
        document.removeEventListener('click', this.foo);
    }

    mounted() {
        if (this.$store.state.eldSelectedperson > 0) {
            this.$store.commit("setEldSelectedperson", 0);
        }

        //setInterval(this.fetchPersondecreesActive, 20000);
    }

    /*random(decree) {
        *//*/if (decree.id == this.previos.id)
            return this.rand_list;*//*
        let time = [];
        fetch('api/MailController/rand', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Structure[]>;
            })
            .then(resualt => {
                time = resualt;
                resualt[0].nameshortened
                this.rand_list = resualt;
            })

    }*/

    delrand(structure) {
        let time = [];
        this.rand_list.forEach(r => {
            if (r.id != structure.id) {
                time.push(r);
            }
        })
        this.rand_list = time;
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value) {
            this.menuid = 2;
            this.fetchMailExplorer()
            //this.fetchPersondecreesActive();
            this.set_menu_id(2);
        }
    }
    fetchMailExplorer() {
        let time_value = this.multipleSelection;
        fetch('api/MailController/Full', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Mailexplorer[]>;
            })
            .then(resualt => {
                this.full_explorers = resualt;
                this.fullpersondecrees = this.fetchPersondecreesActive();
                this.reload();
                this.multipleSelection = time_value;
            })
        return this.fullpersondecrees;
    }

    fetchPersondecreesActive() {
        let time_value = this.multipleSelection;
        let time_explorer = this.full_explorers;
        fetch('api/Persondecree/FullByUser', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Persondecree[]>;
            })
            .then(result => {
                //alert('mark');
                result.forEach(r => {
                    var exp: Mailexplorer = time_explorer.find(q => q.id == r.mailexplorerid);
                    r.creatorfolder = exp.folderCreator;
                    r.ownerfolder = exp.folderOwner;
                    r.accessforreading = exp.accessForReading;
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
        //this.viewpersondecrees.length
    }

    rowClicked(row, flag = true) {
        let k;
        if (this.menuid == 10) {
            this.getexcerts(row);
            //this.$store.commit("setdecreemailM", row.id);
            return;
        }
        if (flag) {
            let list_access: string[] = row.accessforreading.split('_');
            let k = list_access.find(r => parseInt(r) == this.$store.state.user.id);
        } else
            k = "true";
        if (k != undefined || this.$store.state.user.id == 1)
            //this.open(row);
            this.$store.commit("setdecreemailM", row.id);
        else
            this.$notify({
                title: 'Нет прав',
                message: 'Отказано в доступе',
                type: 'warning'
            });
    }

    createMenuToggle() {
        this.fullpersondecrees = null;
        //this.persondecreeCreate();
        fetch('api/MailController/AddNew', {
            method: 'get',
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(response => {
            return response.json() as Promise<Mailexplorer>;
        }).then(resualt => {
            fetch('/api/Persondecree', {
                method: 'post',
                body: JSON.stringify(<Persondecree>{
                    persondecreeManagementStatus: 1, // Добавить
                    nickname: "",
                    mailexplorerid: resualt.id,
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
                        this.fetchMailExplorer();
                        //this.open(result);
                        this.rowClicked(result, false);
                    });
            })
        })
    }

    open(decree: Persondecree) {
        this.$store.commit("setpersondecree", decree);
        this.$store.commit("setdecreeoperationtemplatecreatorVisible", this.$store.state.decreeoperationtemplatecreatorVisible ? false : true);
        this.rowClicked(decree);
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
        this.set_menu_id(this.menuid);
        this.getDateList();
        this.update = false;
        this.update = true;
        /*this.getDateList();*/
    }

    set_menu_id(id: number) {
        this.setMenuid(id);
        /*setTimeout(() => {
            this.setMenuid(id);
        }, 500);*/
    }

    setMenuid(id: number) {
        //this.rand_list = [];
        this.menuid = id;
        this.multipleSelection = [];
        /*this.viewpersondecrees = */this.filterbyfolders(id);
        //this.getDateList();
    }

    switch_k() {
        this.unit_dialog = false;
    }

    FolderSelectedByUnit(id) {
        this.menuunitid = (this.explorerFolder.indexOf(id) + 2);
        this.viewpersondecreesunit = this.filterbyfolders(this.menuunitid);
    }

    addToUnitList(decree: Persondecree) {
        if (this.multipleSelection.find(r => r.id == decree.id) == undefined)
            this.multipleSelection.push(decree);
    }

    filterbyfolders(folder: number) {
        var time = [];
        var excert = false;
        //this.viewpersondecrees = time;
        if (folder == 2) {
            this.fullpersondecrees.forEach(r => {
                if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                    time.push(r);
                } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                    time.push(r);
                }
                /*if (r.creatorfolder == folder - 1 && r.creator == r.owner && r.creator == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)*/
            })
        } else if (folder == 3) {
            this.fullpersondecrees.forEach(r => {
                if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                    time.push(r);
                } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                    time.push(r);
                }
            })
            /*this.fullpersondecrees.forEach(r => {
                if (r.signed == 0 && r.creator != r.owner && r.owner == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)
            })*/
        } else if (folder == 4) {
            this.fullpersondecrees.forEach(r => {
                if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                    time.push(r);
                } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                    time.push(r);
                }
            })
            /*this.fullpersondecrees.forEach(r => {
                if (r.signed == 0 && r.creator != r.owner && r.creator == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)
            })*/
        } else if (folder == 5) {
            this.fullpersondecrees.forEach(r => {
                if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                    time.push(r);
                } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                    time.push(r);
                }
            })
        } else if (folder == 6) {
            this.fullpersondecrees.forEach(r => {
                if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                    time.push(r);
                } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                    time.push(r);
                }
            })
        } else if (folder == 7) {
            this.fullpersondecrees.forEach(r => {
                if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                    time.push(r);
                } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                    time.push(r);
                }
            })
        } else if (folder == 8) {
            this.fullpersondecrees.forEach(r => {
                if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                    time.push(r);
                } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                    time.push(r);
                }
            })
            /*this.fullpersondecrees.forEach(r => {
                if (r.signed == 1 && r.creator == this.$store.state.user.id)
                    this.viewpersondecrees.push(r)
            })*/
        } else if (folder == 9) {
            this.fullpersondecrees.forEach(r => {
                if (r.signed == 1)
                    time.push(r)
            })
        } else if (folder == 10) {
            /*this.fullpersondecrees.forEach(r => {
                if (r.signed == 1)
                    time.push(r)
            })*/
            time = this.loadexcertsdecree();
            //excert = true;
        }
        this.viewpersondecrees = time;
        this.$store.commit("setExcertMenu", excert);
        this.$store.commit("setExcertDecreeId", null);
        this.excertmenu = excert;
        return time;
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
        let explorer = [];
        this.multipleSelection.forEach(r => {
            explorer.push(this.full_explorers.find(h => h.id == r.mailexplorerid))
        })
        fetch('api/MailController/ChangeFolder/' + (this.explorerFolder.indexOf(folder) + 1).toString(),
            {
                method: 'post',
                body: JSON.stringify(explorer),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(x => {
                this.multipleSelection = [];
                this.dialogVisibleExplorer = false;
                this.fetchMailExplorer();
            })
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
        this.unit_dialog = true;
        this.unit_decree = new Persondecree();
        //this.persondecreesUnite();
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
        fetch('api/MailController/Send/' + (id).toString(),
            {
                method: 'post',
                body: JSON.stringify(this.multipleSelection),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(x => {
                this.multipleSelection = [];
                this.fetchMailExplorer();
                this.usersSearch = [];
                this.dialogVisibleSend = false;
            })
        /*this.multipleSelection.forEach(q => {
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
                this.fetchMailExplorer();
                this.usersSearch = [];
                (<any>Vue).notify("S:Проект приказа направлен на кадровика");
            })
        })*/
    }

    closeUserSearch() {
        this.usersSearch = [];
    }

    persondecreesUnite() {
        if (this.unit_decree.nickname == null || this.unit_decree.nickname == "") {
            this.$notify({
                title: 'Проверьте данные',
                message: 'Введите рабочее название приказа',
                type: 'warning'
            });
            return;
            //this.multipleSelection.length;
        }
        let str: string = "1"; // первый номер будет означать тип операции по отношению к выбранным проектам приказов
        this.multipleSelection.forEach(p => {
            str += "_" + p.id;
            var time: Mailexplorer = this.full_explorers.find(r => r.id == p.mailexplorerid);
            if (this.$store.state.user.id == p.creator)
                time.folderCreator = (this.explorerFolder.indexOf("Отработанные") + 1);
            else
                time.folderOwner = (this.explorerFolder.indexOf("Отработанные") + 1);
            fetch('api/MailController/Update', {
                method: 'post',
                body: JSON.stringify(time),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(response => {
                return response.json() as Promise<Mailexplorer>;
            })
        });
        fetch('api/Persondecree/Action' + str, {
            method: 'post',
            body: JSON.stringify(this.unit_decree),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(response => {
            return response.json() as Promise<string>;
        }).then(result => {
            (<any>Vue).notify(result);
            this.fetchMailExplorer();
            this.viewpersondecreesunit = [];
            this.dialogVisibleUnit = false;
        })
    }

    sbdWorker() {
        if (this.$store.state.user.id != 1)
            return;
        fetch('/api/MailController/DefaultWorker', {
            method: 'post',
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        });
    }

    excertClose() {
        this.title_text_excert = this.title_text_excert_template;
        this.$store.commit("setExcertMenu", false);
        this.$store.commit("setLodingExcert", false);
        this.excertmenu = false;
    }

    loadexcertsdecree() {
        var output: Persondecree[] = [];
        fetch('api/Persondecreeoperationexcert/excertdecree', {
            method: 'get',
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(response => {
            return response.json() as Promise<Persondecree[]>;
        }).then(resualt => {
            resualt.forEach(r => {
                output.push(this.decreeParser(r));
            })
            this.viewpersondecrees = output;
            //this.reload();
        });
        return output;
    }

    decreeParser(r) {
        var exp: Mailexplorer = this.full_explorers.find(q => q.id == r.mailexplorerid);
        r.creatorfolder = exp.folderCreator;
        r.ownerfolder = exp.folderOwner;
        r.accessforreading = exp.accessForReading;
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

        return r;
    }

    getexcerts(row) {
        this.$store.commit("setExcertMenu", true);
        this.excertmenu = true;
        this.excertsdecree = row;
        this.title_text_excert = this.title_text_excert_template + ' № ' + this.excertsdecree.getNumber + ' от ' + this.excertsdecree.getDate;
        this.excertsdecreestructure = [];
        fetch('api/Persondecreeoperationexcert/listexcertsstructure/' + row.id, {
            method: 'get',
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(r => {
            return r.json() as Promise<Structure[]>;
        })
            .then(resualt => {
                resualt.forEach(r => {
                    this.excertsdecreestructure.push(r);
                })
            })
    }

    openexcert(struct) {
        this.$store.commit("setLodingExcert", true);
        this.$store.commit("setExcertDecreeId", this.excertsdecree.id.toString() + "_" + struct.id.toString());
    }

    get loading2(): boolean {
        return this.$store.state.lodingexcert;
    }
}