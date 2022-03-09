import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AwesomeMask from 'awesome-mask';
import MaskedInput from 'vue-text-mask';
import Element from 'element-ui';
import { Button, Select, Input } from 'element-ui';
//import 'element-ui/lib/theme-chalk/index.css'
//const MaskedInput = require('vue-text-mask');
//Vue.use(MaskedInput);
Vue.directive('mask', AwesomeMask);
Vue.component('masked-input', MaskedInput);
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Select.name, Select);
Vue.use(Element);



@Component({
    components: {
        Departmentslist: require('../departmentslist/departmentslist.vue.html'),
        Positionslist: require('../positionslist/positionslist.vue.html'),
        Modepanel: require('../modepanel/modepanel.vue.html'),
    }
})
export default class HomeComponent extends Vue {
    access: string;

    mounted() {
        this.fetchAccessInitial().then(c => {
            setInterval(this.fetchAccess, 3000);
            
        });

        window.onbeforeunload = function (e) {
            var dialogText = 'Вы действительно хотите покинуть программу?'
            e.returnValue = dialogText;
            return dialogText;
        };
        //(<any>Vue).getStructureAll();
    }


    async fetchAccessInitial() {
        this.access = await (<any>Vue).getAccessStatus();
        (<any>Vue).requireLogin(this.access);
    }

    async fetchAccess() {
        //this.access = await (<any>Vue).getAccessStatus();
        //(<any>Vue).requireLogin(this.access);

        (<any>Vue).requireLoginNew();
    }

    get departmentsListId() {
        return this.$store.state.departmentsListId;
    }

    get positionsListId() {
        return this.$store.state.positionsListId;
    }

    get positionsListVisible() {
        return this.$store.state.positionsListId != null && !isNaN(this.$store.state.positionsListId) && this.$store.state.positionsListId != 0; 
    }

    get modepanelVisible() {
        return this.$store.state.modepanelVisible;
    }

    get currentStructure() {
        return this.$store.state.currentStructure;
    }

    close() {
        this.$store.commit("setdecreeoperationtemplatecreatorVisible", false);
        //this.$store.commit("setmailmodeprevios", false);
    }
}