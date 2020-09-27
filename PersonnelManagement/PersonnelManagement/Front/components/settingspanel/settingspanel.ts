import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import Element from 'element-ui';
import $ from "jquery";
import { Button, Select, Input, Collapse, CollapseItem, Switch, Notification, TabPane, Tabs, Checkbox } from 'element-ui';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Select.name, Select);
Vue.component(Collapse.name, Collapse);
Vue.component(CollapseItem.name, CollapseItem);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Switch.name, Switch);
Vue.component(Tabs.name, Tabs);
Vue.component(TabPane.name, TabPane);
Vue.use(Element);



const fetchUserDelay: number = 3000;
const updateDelayAfterEdit: number = 8000;

@Component({
    components: {

    }
})
export default class AdminpanelComponent extends Vue {

    timemachineDate: string;
    positionCompact: boolean;
    sidebarDisplay: boolean;

    @Prop({ default: false })
    visible: boolean;
    activeName: string;

    data() {
        return {
            visible: false,
            activeName: "first",
            timemachineDate: this.$store.state.date,
            positionCompact: false,
            sidebarDisplay: false,
        }
    }

    handleClick(tab, event) {
        
    }

    mounted() {
        if (this.$store.state.positioncompact > 0) {
            this.positionCompact = true;
        }
        if (this.$store.state.sidebarDisplay > 0) {
            this.sidebarDisplay = true;
        }
    }

    appearanceChange() {
        let positionCompactVal: number = 0;
        let sidebarDisplay: number = 0;
        if (this.positionCompact) {
            positionCompactVal = 1;
        }
        if (this.sidebarDisplay) {
            sidebarDisplay = 1;
        }
        let appearance = {
            positioncompact: positionCompactVal,
            sidebardisplay: sidebarDisplay,
        }

        this.$store.commit("updateUserAppearance", appearance);
    }

    saveChanges() {
        this.$store.commit("setDateByInput", this.timemachineDate);
        //this.timemachineDate = this.$store.state.date;
    }

    restoreCurrentDate() {
        this.$store.commit("setDateByInput", (<any>Vue).toDateInputValue(new Date()));
        this.timemachineDate = (<any>Vue).toDateInputValue(new Date());
    }



}