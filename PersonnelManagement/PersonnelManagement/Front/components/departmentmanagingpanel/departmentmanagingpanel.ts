import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button } from 'element-ui';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.use(Element);

//public string Type { get; set; }
//public string Name { get; set; }
//public int ? Parent { get; set; }

class DepartmentManagement {
    type: string;
    structure: number;
    name: string;
    parent: number;
    datecustom: number;
    dateactive: Date;
}

@Component({

})
export default class DepartmentmanagingpanelComponent extends Vue {
    status: string;
    name: string;
    datecustom: boolean;
    dateactive: string;
    // Statuses
    // addnewdepartment
    // removedepartment
    // renamedepartment

    @Prop({ default: 'Dr' })
    type: string;

    @Prop({ default: null })
    structure: string;

    @Prop({ default: null })
    parent: string;

    @Prop({ default: false })
    visible: boolean;

    data() {
        return {
            status: "renamedepartment",
            type: "",
            structure: "",
            parent: "",
            name: "",
            visible: false,
            datecustom: false,
            dateactive: this.toDateInputValue(new Date()),
        }
    }

    get intromessage() {
        switch (this.type) {
            case "addnewdepartment": return "Создать отдел";
            case "removedepartment": return "Вы уверены, что хотите упразднить отдел?";
            case "renamedepartment": return "Переименовать отдел";
            default: return "Произошла непредвиденная оказия";
        }
    }

    get getplaceholdername() {
        switch (this.type) {
            case "addnewdepartment": return "Название отдела";
            case "removedepartment": return "Название отдела";
            case "renamedepartment": return "Новое название отдела";
            default: return "Произошла непредвиденная оказия";
        }
    }

    get displayinput() {
        switch (this.type) {
            case "addnewdepartment": return false;
            case "removedepartment": return true;
            case "renamedepartment": return false;
            default: return true;
        }
    }

    mounted() {
        (<any>this.$refs.inputdepartmentmanagementname).focus();
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        this.datecustom = false;
        this.dateactive = this.toDateInputValue(new Date());

        (<any>this.$refs.inputdepartmentmanagementname).focus();
    }

    okbutton() {
        let datecustomNum: number = 0;
        if (this.datecustom) {
            datecustomNum = 1;
        }

        //alert(this.parent);
        fetch('/api/Departments', {
            method: 'post',
            body: JSON.stringify(<DepartmentManagement>{
                type: this.type, parent: Number.parseInt(this.parent), name: this.name, structure: Number.parseInt(this.structure), datecustom: datecustomNum, dateactive: new Date(this.dateactive)
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                //this.fetchUsers();
                //(<any>Vue).notify(response);
                //(<any>Vue).forceStructureUpdate = true;
                this.$store.commit("setForceDepartmentUpdate", true);
                this.$emit('update:visible', false);
            });

    }

    cancelbutton() {
        this.$emit('update:visible', false);
        //this.visible = false;
        //alert(this.dialogvisible);
    }

    toDateInputValue(date: Date): string {
        var local = new Date(date);
        local.setMinutes(date.getMinutes() - date.getTimezoneOffset());
        return local.toJSON().slice(0, 10);
    }

}