import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import download from 'downloadjs';
import Rank from '../../classes/rank'
import Sourceoffinancing from '../../classes/sourceoffinancing'
import { Input, Button, Checkbox, Select, Option } from 'element-ui';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Select.name, Select);
Vue.component(Option.name, Option);
Vue.use(Element);

@Component({

})
export default class DecreehistoryComponent extends Vue {

    @Prop({ default: '1' })
    type: string;

    @Prop({ default: null })
    id: string;

    @Prop({ default: false })
    visible: boolean;

    data() {
        return {
            type: "",
            id: "",
            visible: false,
        }
    }

    mounted() {

    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {

    }

}