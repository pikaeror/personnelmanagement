import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import download from 'downloadjs';
import Rank from '../../classes/rank'
import Sourceoffinancing from '../../classes/sourceoffinancing'
import Positiontype from '../../classes/positiontype';
import Positioncategory from '../../classes/positioncategory';
import Mrd from '../../classes/mrd';
import Positionmrd from "../../classes/positionmrd";
import Altrank from '../../classes/altrank';
import Altrankcondition from '../../classes/altrankcondition';
import Altrankconditiongroup from '../../classes/altrankconditiongroup';
import StructureTree from '../../classes/structuretree';
import Structuretype from '../../classes/structuretype';
import Pmrequest from '../../classes/pmrequest';
import Pmresult from '../../classes/pmresult';
import { Input, Button, Checkbox, Select, Option, Dialog } from 'element-ui';
import Structureregion from '../../classes/structureregion';
import Structure from '../../classes/Structure';
import formatting from '../../classes/formating_functions/el_table_formatter';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Select.name, Select);
Vue.component(Dialog.name, Dialog);
Vue.component(Option.name, Option);
Vue.use(Element);

class Pmresultsinglerank {
    rank: string;
    defaultcount: number;
    defaultcountvar: number;
    absolutecount: number;
    maxcount: number;
    mincount: number;
    reduce: number;
    uprank: string;
    uprankready: string;
    sumunited: number[];
    uprankmappart: number[];
    upinfos: Upinfo[];
    cometos: Upinfo[];
    unitedlengthmax: number;
}

class Uprankmap {
    groupid: number;
    value: number;
}

class Upinfo {
    name: string;
    up: number;
    value: number;
}

class Civil {
    name: string;
    count: number;
}

@Component({

})
export default class PmrequestComponent extends Vue {
    type: number;
    options: any[];

    rank: any[];
    ranksexpanded: boolean;
    civil: Civil[];
    absoluteup: number;
    sof: any;
    positiontype: any[];
    positiontypeTitle: string;
    positioncategory: any;
    positioncategoryDisabled: boolean;
    positioncategoriesFiltred: Positioncategory[];
    
    positioncategoriesCivil: Positioncategory[];
    mrd: any;
    notice: string;
    structuretype: any[];
    replacedbycivil: boolean;
    replacedbycivilnot: boolean;
    signed: boolean;
    notsigned: boolean;
    willbenotsigned: boolean; 
    willbesigned: boolean;
    decertificateexpired: boolean;
    decertificate: boolean;
    civilclasshigh: number;
    civilclasslow: number;
    replacedbycivildateavailable: boolean;
    replacedbycivildateexpired: boolean;

    structurerank: any[];
    structureregion: any[];
    structurecity: string;
    structurestreet: string;

    structurelist: number[];
    structureTrees: StructureTree[];
    structureselectionprocess: boolean;

    displaytreeseparately: boolean;
    displaytree: boolean;
    displaystructureparent: boolean;
    displaypositionchildren: boolean;
    displaymrds: boolean;
    displayreplacedbycivilinfo: boolean;

    exceldialogvisible: boolean;
    count: number;
    countvar: number;
    loading: boolean;

    pmresultsingleranks: Pmresultsinglerank[];
    pmresultsingleranksdisplay: boolean;
    newdisplay: boolean;
    civilonly: any;
    notopchs: boolean;

    structurecountmode: boolean;
    structurecountallinclusive: boolean;
    structuresub: boolean; // Включать подчиненные подразделения тех подразделений, что прошли фильтрацию
    structuresublevel: number; // Уровень вложенности для пункта выше
    structureselfcount: boolean; // Учитывать исключительно собственную численность подразделений, не включая численночть подчиненных


    @Prop({ default: false })
    visible: boolean;

    @Prop({ default: false })
    visiblevar: boolean;

    data() {
        return {
            options: [{
                value: '1',
                label: 'Запрос по должностям'
            },
            {
                value: '2',
                label: 'Запрос по подразделениям'
            }],
            type: null,
            rank: [],
            ranksexpanded: false,
            civil: [],
            absoluteup: 0,
            sof: [],
            positiontype: [],
            positiontypeTitle: "",
            positioncategory: null,
            positioncategoryDisabled: false,
            positioncategoriesFiltred: [],
            
            positioncategoriesCivil: [],
            mrd: [],
            quantity: 1,
            notice: "",
            structuretype: [],

            replacedbycivil: false,
            replacedbycivilnot: false,
            signed: false,
            notsigned: false,
            willbenotsigned: false,
            willbesigned: false,
            decertificateexpired: false,
            decertificate: false,
            civilclasshigh: null,
            civilclasslow: null,
            replacedbycivildateavailable: false,
            replacedbycivildateexpired: false,

            structurerank: [],
            structureregion: [],
            structurecity: "",
            structurestreet: "",

            structurelist: [],
            structureTrees: [],
            structureselectionprocess: false,

            displaytreeseparately: false,
            displaytree: false,
            displaystructureparent: false,
            displaypositionchildren: false,
            displaymrds: false,
            displayreplacedbycivilinfo: false,

            exceldialogvisible: false,
            count: 0,
            countvar: 0,
            loading: false,

            pmresultsingleranks: [],
            pmresultsingleranksdisplay: false,
            newdisplay: true,
            civilonly: false,
            notopchs: false,

            structurecountmode: false,
            structurecountallinclusive: false,
            structuresub: false, // Включать подчиненные подразделения тех, кто прошел фильтрацию
            structuresublevel: 0, // Уровень вложенности для пункта выше
            structureselfcount: false, // Учитывать исключительно собственную численность
        }
    }

    formatting = formatting;

    mounted() {
        // setInterval(this.load_educations_parameters, 10000);
    }

    get positiontypes(): Positiontype[] {
        return this.$store.state.positiontypes;
    }

    get structuretypes(): Structuretype[] {
        return this.$store.state.structuretypes;
    }

    get structureregions(): Structureregion[] {
        return this.$store.state.structureregions;
    }

    get positioncategories(): Positioncategory[] {
        return this.$store.state.positioncategories;
    }

    get sofs(): Sourceoffinancing[] {
        return this.$store.state.sofs;
    }

    get mrds(): Mrd[] {
        return this.$store.state.mrds;
    }

    get ranks(): Rank[] {
        return this.$store.state.ranks;
    }


    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value && this.structureselectionprocess) {
            if (this.$store.state.modeselectedstructure > 0) {
                if (this.structurelist.find(c => c == this.$store.state.modeselectedstructure) == null) {
                    this.structurelist.push(this.$store.state.modeselectedstructure);
                }
                this.$store.commit("setModeselectedstructure", 0);
            }
            this.prepareTrees();
            this.structureselectionprocess = false;
            return;
        }
        if (this.structureselectionprocess) {
            return;
        }
    }

    tablerequest() {
        let p = 2;
    }

    request() {
        /*this.exceldialogvisible = true;
        if (true == true) {
            return;
        }*/
        if (this.type == 3) {
            this.tablerequest()
            return;
        }
        this.loading = true;
        let structuretypesString: string = this.arrayToString(this.structuretype);
        let positiontypesString: string = this.arrayToString(this.positiontype);
        let positioncategoriesString: string = this.arrayToString(this.positioncategory);
        let sofsString: string = this.arrayToString(this.sof);
        let mrdsString: string = this.arrayToString(this.mrd);
        let ranksString: string = this.arrayToString(this.rank);
        let structurerankString: string = this.arrayToString(this.structurerank);
        let structureregionString: string = this.arrayToString(this.structureregion);
        let structureArray: Number[] = new Array();
        this.structureTrees.forEach(st => {
            structureArray.push(st.id);
        })
        let structureString: string = this.arrayToString(structureArray);

        let replacedbycivilNum: number = 0;
        if (this.replacedbycivil) {
            replacedbycivilNum = 1;
        }

        let replacedbycivilnotNum: number = 0;
        if (this.replacedbycivilnot) {
            replacedbycivilnotNum = 1;
        }

        let signedNum: number = 0;
        if (this.signed) {
            signedNum = 1;
        }

        let notsignedNum: number = 0;
        if (this.notsigned) {
            notsignedNum = 1;
        }

        let willbesignedNum: number = 0;
        if (this.willbesigned) {
            willbesignedNum = 1;
        }

        let willbenotsignedNum: number = 0;
        if (this.willbenotsigned) {
            willbenotsignedNum = 1;
        }

        let decertificateNum: number = 0;
        if (this.decertificate) {
            decertificateNum = 1;
        }

        let decertificateexpiredNum: number = 0;
        if (this.decertificateexpired) {
            decertificateexpiredNum = 1;
        }

        let displaytreeseparatelyNum: number = 0;
        if (this.displaytreeseparately) {
            displaytreeseparatelyNum = 1;
        }



        if (this.structurecity == null) {
            this.structurecity = "";
        }

        if (this.structurestreet == null) {
            this.structurestreet = "";
        }

        let civilclasshighNum: number = 0;
        if (this.civilclasshigh != null) {
            civilclasshighNum = this.civilclasshigh;
        }

        let civilclasslowNum: number = 0;
        if (this.civilclasslow != null) {
            civilclasslowNum = this.civilclasslow;
        }

        let replacedbycivildateavailableNum: number = 0;
        if (this.replacedbycivildateavailable) {
            replacedbycivildateavailableNum = 1;
        }

        let replacedbycivildateexpiredNum: number = 0;
        if (this.replacedbycivildateexpired) {
            replacedbycivildateexpiredNum = 1;
        }

        let ranksexpanedNum: number = 0;
        if (this.ranksexpanded) {
            ranksexpanedNum = 1;
        }

        let civilonlyNum: number = 0;
        if (this.civilonly) {
            civilonlyNum = 1;
        }

        let notopchsNum: number = 0;
        if (this.notopchs) {
            notopchsNum = 1;
        }

        let structurecountmodeNum: number = 0;
        if (this.structurecountmode) {
            structurecountmodeNum = 1;
        }

        let structurecountallinclusiveNum: number = 0;
        if (this.structurecountallinclusive) {
            structurecountallinclusiveNum = 1;
        }

        let structuresubNum: number = 0;
        if (this.structuresub) {
            structuresubNum = 1;
        }

        let structureselfcountNum: number = 0;
        if (this.structureselfcount) {
            structureselfcountNum = 1;
        }

        //structuresub: boolean; // Включать подчиненные подразделения тех подразделений, что прошли фильтрацию
        //structuresublevel: number; // Уровень вложенности для пункта выше
        //structureselfcount: boolean; // Учитывать исключительно собственную численность подразделений, не включая численночть подчиненных
        fetch('/api/Pmrequest', {
            method: 'post',
            body: JSON.stringify(<Pmrequest>{
                type: this.type,
                positiontypes: positiontypesString,
                positioncategories: positioncategoriesString,
                sofs: sofsString,
                structures: structureString,
                structuretypes: structuretypesString,
                mrds: mrdsString,
                replacedbycivil: replacedbycivilNum,
                replacedbycivilnot: replacedbycivilnotNum,
                signed: signedNum,
                willbesigned: willbesignedNum,
                willbenotsigned: willbesignedNum,
                decertificate: decertificateNum,
                decertificateexpired: decertificateexpiredNum,
                displaytreeseparately: displaytreeseparatelyNum,
                ranks: ranksString,
                civilclasshigh: civilclasshighNum,
                civilclasslow: civilclasslowNum,
                structurecity: this.structurecity,
                structurerank: structurerankString,
                structureregion: structureregionString,
                structurestreet: this.structurestreet,
                replacedbycivildateavailable: replacedbycivildateavailableNum,
                replacedbycivildateexpired: replacedbycivildateexpiredNum,
                ranksexpanded: ranksexpanedNum,
                civil: civilonlyNum,
                notopchs: notopchsNum,
                structurecountmode: structurecountmodeNum,
                structurecountallinclusive: structurecountallinclusiveNum,
                structuresub: structuresubNum,
                structuresublevel: this.structuresublevel,
                structureselfcount: structureselfcountNum,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(x => x.json() as Promise<Pmresult>)
            .then(x => {
                this.count = x.count;
                this.countvar = x.countvar;
                this.exceldialogvisible = true;
                this.loading = false;

                this.pmresultsingleranksdisplay = !x.ranksexpanded;
                //if (!x.ranksexpanded) {
                    //alert(this.pmresultsingleranksdisplay);
                this.pmresultsingleranks = new Array();
                
                let ranknames: string[] = x.ranks.split('@');
                let defaultcount: string[] = x.defaultcount.split('@');
                let defaultcountvar: string[] = x.defaultcountvar.split('@');
                let absolutecount: string[] = x.absolutecount.split('@');
                let maxcount: string[] = x.maxcount.split('@');
                let mincount: string[] = x.mincount.split('@');
                let uprankready: string[] = x.uprankready.split('&');
                let uprankunited: string[] = x.uprankunited.split('@');
                let comefromunited: string[] = x.comefromunited.split('@');
                let unitedlengthmax: string[] = x.unitedlengthmax.split('@');
                let sumunited: string[] = x.sumunited.split('@');
                let uppedcount: string[] = null;
                let uppedmap: string[] = null;
                if (x.ranksexpanded > 0) {
                    uppedcount = x.uppedcount.split('@'); //При наличии классности=1:100;2:120&При наличии ученой степени=1:5, 
                    uppedmap = x.uppedmap.split('@'); // Карта: Название=Айди;На сколько ранков максимум доступен подъем.  При наличии классности=1;2,
                }
                this.absoluteup = 0;

                this.civil = new Array();
                x.civil.split('@').forEach(s => {
                    let civilEl: Civil = new Civil();
                    if (s.length > 0) {
                        civilEl.name = s.split("=")[0];
                        civilEl.count = Number.parseFloat(s.split("=")[1].replace(',','.'));
                        this.civil.push(civilEl);
                    }
                    
                })

                for (let i: number = 0; i < ranknames.length; i++) {
                    let singlerank: Pmresultsinglerank = new Pmresultsinglerank();
                    let ranksplittedname: string[] = ranknames[i].split(" ");
                    ranksplittedname[0] = ranksplittedname[0].toLowerCase();
                    ranksplittedname.pop(); ranksplittedname.pop();

                    singlerank.rank = ranksplittedname.join(' ');

                    singlerank.defaultcount = Number.parseInt(defaultcount[i]);
                    singlerank.defaultcountvar = Number.parseInt(defaultcountvar[i]);
                    singlerank.absolutecount = Number.parseInt(absolutecount[i]);
                    singlerank.maxcount = Number.parseInt(maxcount[i]);
                    singlerank.mincount = Number.parseInt(mincount[i]);
                    singlerank.uprankready = uprankready[i];
                    singlerank.uprankmappart = new Array();

                    if (uppedcount != null) {
                        if (uppedcount[i].length > 0) {
                            uppedcount[i].split('&').forEach(p => {
                                p.split('=')[1].split(';').forEach(pp => {
                                    //alert('hash');
                                    singlerank.uprankmappart.push(Number.parseInt(pp.split(':')[1]));
                                })
                            })
                        }
                        
                    }

                    singlerank.unitedlengthmax = Number.parseInt(unitedlengthmax[i]);
                    if (singlerank.unitedlengthmax > this.absoluteup) {
                        this.absoluteup = singlerank.unitedlengthmax;
                    }

                    // Капитан внутренней службы:1:5;Майор внутренней службы:2:5;
                    let upinfos: Upinfo[] = new Array();
                    //alert(uprankunited[i]);
                    if (uprankunited[i].length > 0) {
                        //alert(uprankunited[i]);
                        uprankunited[i].split(';').forEach(e => {
                            let upinfo: Upinfo = new Upinfo();
                            let splitted: string[] = e.split(':');
                            let splittedname: string[] = splitted[0].split(" ");
                            splittedname[0] =  splittedname[0].toLowerCase();
                            splittedname.pop(); splittedname.pop();

                            upinfo.name = splittedname.join(' ');
                            upinfo.up = Number.parseInt(splitted[1]);
                            upinfo.value = Number.parseInt(splitted[2]);
                            upinfos.push(upinfo);
                        })
                        
                    }
                    singlerank.upinfos = upinfos;

                    // Старший лейтенант внутренней службы:1:5;Капитан внутренней службы:2:5;
                    let cometos: Upinfo[] = new Array();
                    //alert(comefromunited[i]);
                    if (comefromunited[i].length > 0) {
                        comefromunited[i].split(';').forEach(e => {
                            let upinfo: Upinfo = new Upinfo();
                            let splitted: string[] = e.split(':');
                            let splittedname: string[] = splitted[0].split(" ");
                            splittedname[0] = splittedname[0].toLowerCase();
                            splittedname.pop(); splittedname.pop();

                            upinfo.name = splittedname.join(' ');
                            upinfo.up = Number.parseInt(splitted[1]);
                            upinfo.value = Number.parseInt(splitted[2]);
                            cometos.push(upinfo);
                        })
                        singlerank.cometos = cometos;
                    }

                    if (sumunited[i].length > 0) {
                        let sumunitedPart: number[] = new Array();
                        //alert(sumunited[i]);
                        sumunited[i].split(':').forEach(e => {
                            sumunitedPart.push(Number.parseInt(e));
                        })
                        singlerank.sumunited = sumunitedPart;
                    }
                    

                    this.pmresultsingleranks.push(singlerank);
                    //this.pmresultsingleranks = this.temporarySwap(this.pmresultsingleranks);
                    //this.pmresultsingleranks = this.pmresultsingleranks.reverse();
                    //this.pmresultsingleranks = this.pmresultsingleranks=> [..this.pmresultsingleranks].map(this.pmresultsingleranks.pop, this.pmresultsingleranks);
                }
                this.pmresultsingleranks.reverse();
                //} else {

                //}
            })
    }

    temporarySwap(array) {
        var left = null;
        var right = null;
        var length = array.length;
        for (left = 0, right = length - 1; left < right; left += 1, right -= 1) {
            var temporary = array[left];
            array[left] = array[right];
            array[right] = temporary;
        }
        return array;
    }

    getUpinfo(singlerank: Pmresultsinglerank, index: number): Upinfo {
        index -= 1;
        if (singlerank.upinfos != null && singlerank.upinfos.length > index) {
            return singlerank.upinfos[singlerank.upinfos.length - 1 - index];
        } else {
            return null;
        }
    }

    getComefrominfo(singlerank: Pmresultsinglerank, index: number): Upinfo {
        index -= 1;
        if (singlerank.cometos != null && singlerank.cometos.length > index) {
            return singlerank.cometos[singlerank.cometos.length - 1 - index];
        } else {
            return null;
        }
    }

    downloadExcel() {
        let today: Date = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        let ddString = dd.toString();
        if (dd < 10) {
            ddString = '0' + dd;
        }

        let mmString = mm.toString();
        if (mm < 10) {
            mmString = '0' + mm;
        }

        fetch('/api/Pmrequest', {
            method: 'post',
            body: JSON.stringify(<Pmrequest>{
                type: 5
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(x => x.blob())
            .then(x => {
                download(x, "Запрос_" + ddString + mmString + yyyy);
                this.exceldialogvisible = false;
            })
    }

    arrayToString(array: any[]): string {
        if (array == null || array.length == 0) {
            return "";
        }
        let stringArray: string = "";
        array.forEach(e => {
            stringArray += e + ",";
        })
        stringArray = stringArray.slice(0, -1);

        return stringArray;
    }

    shortifyTitle(title: string): string {
        return (title.length > 90) ? title.substr(0, 90 - 1) + '...' : title;
    }

    addStructure() {
        this.structureselectionprocess = true;
        this.$store.commit("setModeselectstructure", true);
        //this.$emit('update:visible', false);
    }

    removeStructure(id: number) {
        this.structurelist = this.structurelist.filter(c => c != id);
        this.structureTrees = this.structureTrees.filter(c => c.id != id);
    }


    prepareTrees() {
        let getString: string = "";
        this.structurelist.forEach(c => {
            getString += c + "&";
        })
        if (getString.length > 0) {
            getString = getString.slice(0, -1);
            fetch('api/DetailedStructure/Trees' + getString, { credentials: 'include' })
                .then(response => response.json() as Promise<StructureTree[]>)
                .then(data => {
                    this.structureTrees = data;
                    
                });
        }
    }

    tableautowidth(count: number): string {
        let prop: number = 100 / count;
        return "width:" + prop + "%;";
    }

    clear(list) {
        if (list == this.structuretype)
            this.structuretype = [];
        else if (list == this.positiontype)
            this.positiontype = [];
        else if (list == this.structureTrees)
            this.structureTrees = [];
        else if (list == this.rank)
            this.rank = [];
/*        else if (list == this.positioncategory)
            this.positioncategory = null;*/
        else if (list == this.sof)
            this.sof = [];
        else if (list == this.mrd)
            this.mrd = [];
        else if (list == this.structurerank)
            this.structurerank = [];
        else if (list == this.structureregion)
            this.structureregion = [];
        else
            list = [];
    }
}