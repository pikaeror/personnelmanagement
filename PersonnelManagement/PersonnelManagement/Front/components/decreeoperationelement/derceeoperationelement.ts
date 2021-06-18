import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import ElementUI, { Input, Button, Dropdown, Dialog, DropdownItem, DropdownMenu, Popover, Checkbox, Tooltip, Upload, Autocomplete, Table, TableColumn, DatePicker } from 'element-ui';
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
import ExcertStructures from '../../classes/Excerts/excertStructures';
import { ElTable } from 'element-ui/types/table';
import Datepicker from 'element-ui/types/date-picker';
import elementLocale from 'element-ui/lib/locale/lang/en';
/*import { registerLocale } from "../../../../node_modules/react-datepicker";
import ru from "../../../../node_modules/date-fns/locale/ru";
registerLocale("ru", ru);*/

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
Vue.use(DatePicker)

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
    persondecreearchive: Persondecree[];

    multipleSelection: Persondecree[];
    decreeunopen: Persondecree[];

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
    excertsdecreestructureT: ExcertStructures[];
    title_text_excert: string;
    title_text_excert_template: string;

    new_excert_list: boolean;
    arvhive: boolean;

    upload: number;

    date_range: Date[];
    date_range_start: string;
    date_range_end: string;

    data() {
        return {
            /*ru: {
                language: 'Russian',
                    months: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
                    monthsAbbr: ['Янв', 'Февр', 'Март', 'Апр', 'Май', 'Июнь', 'Июль', 'Авг', 'Сент', 'Окт', 'Нояб', 'Дек'],
                        days: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'], rtl: false, ymd: false, yearSuffix: ''
            },*/

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
            persondecreearchive: [],

            multipleSelection: [],
            decreeunopen: [],

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
            excertsdecreestructureT: [],

            new_excert_list: false,
            arvhive: false,

            upload: 0,

            date_range: [],
            date_range_start: "",
            date_range_end: "",
        }
    }

    /*tables_reload() {
        this.$refs.alltables.refresh();
    }*/

    tableRowClassName({ row, rowIndex }) {
        if (row.signed == 1) {
            return 'success-row';
        }
        if (this.decreeunopen.find(r => r.id == row.id) != undefined) {
            return 'unopen-row';
        }
        return 'all';
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
                this.fullpersondecrees = this.filter_decrees_by_data(result, this.date_range);
                this.reload();
                this.multipleSelection = time_value;
            });
        fetch('api/MailController/unopen', { credentials: 'include' })
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
                this.decreeunopen = result;
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
            k = list_access.find(r => parseInt(r) == this.$store.state.user.id);
        } else
            k = "true";
        if (k != undefined || this.$store.state.user.id == 1 || this.$store.state.user.id == 24) {
            //this.open(row);
            this.$store.commit("setdecreemailM", row.id);
            fetch('api/MailController/opendecree/' + row.id, {
                method: 'post',
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(response => {
                this.fetchPersondecreesActive();
            })
        }
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
        var flag = false;
        if (this.menuid != id) {
            flag = true;
        }
        this.setMenuid(id);
        /*setTimeout(() => {
            this.setMenuid(id);
        }, 500);*/
    }

    setMenuid(id: number) {
        //this.rand_list = [];
/*        this.$store.commit("setExcertMenu", false);
        this.$store.commit("setExcertDecreeId", null);
        this.excertsdecreestructureT = [];*/
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
        this.viewpersondecreesunit = this.filter_my_documents(this.menuunitid);
    }

    addToUnitList(decree: Persondecree) {
        if (this.multipleSelection.find(r => r.id == decree.id) == undefined)
            this.multipleSelection.push(decree);
    }

    filter_my_documents(folder: number = 2) {
        var time = [];
        this.fullpersondecrees.forEach(r => {
            if (r.creator == this.$store.state.user.id && folder - 1 == r.creatorfolder) {
                time.push(r);
            } else if (r.owner == this.$store.state.user.id && folder - 1 == r.ownerfolder) {
                time.push(r);
            }
        })
        return time;
    }

    count_of_unopened(folder: number = 2) {
        var time = this.filter_my_documents(folder);
        var output = [];
        time.forEach(r => {
            this.decreeunopen.forEach(t => {
                if (r.id == t.id)
                    output.push(r);
            });
        })
        return output.length;
    }

    date_range_pick() {
        if (this.date_range != null)
            this.date_range = [new Date(this.date_range_start), new Date(this.date_range_end)]
        this.fetchPersondecreesActive();
    }
    data_range_default() {
        this.date_range = null;
        this.date_range_pick();
    }

    filter_decrees_by_data(data: Persondecree[], date: Date[]): Persondecree[] {
        if (date == null || date.length < 2) {
            var min = Number.MAX_VALUE, max = Number.MIN_VALUE;
            for (var k of data) {
                var datedecree: string[] = k.getDate.split('-');
                var f = new Date(datedecree.reverse().join('-')).valueOf();
                if (f <= min)
                    min = f;
                if (f >= max)
                    max = f;
            }
            this.date_range = [new Date(min), new Date(max)];
            this.date_range_start = new Date(min).toISOString().split('T')[0];
            this.date_range_end = new Date(max).toISOString().split('T')[0];
            return data;
        }
        var output: Persondecree[] = [];
        var fil = [date[0].valueOf(), date[1].valueOf()];
        for (var k of data) {
            var datedecree: string[] = k.getDate.split('-');
            var f = new Date(datedecree.reverse().join('-')).valueOf();
            if (f >= fil[0] && f <= fil[1])
                output.push(k);
        }
        /*var t = data.filter(r => {
            Date.parse(r.getDate).valueOf() >= date[0].valueOf() && Date.parse(r.getDate).valueOf() <= date[1].valueOf()
        });*/
        return output;
    }

    filterbyfolders(folder: number) {
        //this.filter_decrees_by_data(this.fullpersondecrees, this.date_range);
        var time = [];
        var excert = false;
        var new_excert_list = false;
        var archive = false;
        if (folder >= 2 && folder < 9) {
            time = this.filter_my_documents(folder);
        } else if (folder == 9) {
            // new_excert_list = this.new_excert_list ? true : false;
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
        } else if (folder == 99) {
            this.getexcerts(this.excertsdecree, false);
            new_excert_list = true;

        } else if (folder == 98) {
            this.decreeunopen.forEach(r => {
                this.fullpersondecrees.forEach(d => {
                    if (d.id == r.id)
                        time.push(d);
                })
            })
        } else if (folder == 97) {
            time = this.persondecreearchive;
            archive = true;
        }
        //(this.$refs.multipleTable as ElTable).$el).onresize();
        /*if (this.$refs) {
            
        }*/
        //this.reload();
        //this.$emit('input', { plate: plateElement.value });
        this.arvhive = archive;
        this.viewpersondecrees = time;
        this.$store.commit("setExcertMenu", excert);
        this.$store.commit("setExcertDecreeId", null);
        this.excertmenu = excert;
        this.new_excert_list = new_excert_list;
        this.update = true;
        return time;
    }

    getarchive(decree) {
        this.persondecreearchive = [];
        fetch('api/MailController/archive/' + decree, { credentials: 'include' })
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
                this.persondecreearchive = result;
                this.reload();
            });
        return this.persondecreearchive;
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
        this.new_excert_list = false;
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

    getexcerts(row, flag = true) {
        if (flag) {
            this.$store.commit("setExcertMenu", true);
            this.excertmenu = true;
            this.excertsdecree = row;
            this.title_text_excert = this.title_text_excert_template + ' № ' + this.excertsdecree.getNumber + ' от ' + this.excertsdecree.getDate;
            this.excertsdecreestructure = [];
            this.excertsdecreestructureT = [];
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
        } else {
            this.excertsdecree = row;
            this.title_text_excert = this.title_text_excert_template + ' № ' + this.excertsdecree.getNumber + ' от ' + this.excertsdecree.getDate;
        }
        fetch('api/Persondecreeoperationexcert/listexcertsstructuretype/' + row.id, {
            method: 'get',
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(r => {
            return r.json() as Promise<ExcertStructures[]>;
        })
            .then(resualt => {
                if (resualt.length == 0) {
                    this.title_text_excert = this.title_text_excert.indexOf("отсутствуют") == -1 ? this.title_text_excert + " отсутствуют" : this.title_text_excert;
                    //this.title_text_excert += " отсутствуют";
                }
                resualt.forEach(r => {
                    this.excertsdecreestructureT.push(r);
                })
            })
    }

    openexcert(struct) {
        fetch('api/Persondecreeoperationexcert/updateexcert', {
            method: 'post',
            body: JSON.stringify(struct),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
        if (!this.$store.state.excertmenu)
            this.$store.commit("setExcertMenu", true);
        this.$store.commit("setLodingExcert", true);
        this.$store.commit("setExcertDecreeId", this.excertsdecree.id.toString() + "_" + struct.structure.id.toString());
    }

    get loading2(): boolean {
        return this.$store.state.lodingexcert;
    }

    qwerty(row, column, cell, event) {
        this.title_text_excert = this.title_text_excert_template;
        this.excertsdecreestructureT = [];
        if ((this.menuid == 8 || this.menuid == 9)) {
            if (cell.cellIndex == 4) {
                this.new_excert_list = true;
                this.getexcerts(row, false);
                this.set_menu_id(99);
                /*this.menuid = 99;*/
            } else if (cell.cellIndex == 5) {
                this.arvhive = true;
                this.getarchive(row.id);
                this.set_menu_id(97);
            }
        }
    }

    getusers() {
        fetch('api/Users', { credentials: 'include' })
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

    viewUnopeneddecree() {
        if (this.menuid <= 6)
            this.set_menu_id(98);
    }

    isopeneddecree(row) {
        var flag = false
        this.decreeunopen.forEach(r => {
            if (r.id == row.row.id) {
                flag = true;
                return;
            }
        });
        return flag;
    }

    open_history(item) {
        return;
    }

    update_full_decrees() {
        this.fetchPersondecreesActive();
        this.set_menu_id(this.menuid);
    }
}