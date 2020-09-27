import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Element from 'element-ui';
import { Button, Select, Input } from 'element-ui';

Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Select.name, Select);
Vue.use(Element);

const sessionidPrefix: string = "sessionid";

//Vue.arguments.('../notemplate/notemplate.vue.html');
@Component({

})
export default class LoginComponent extends Vue {
    login: string;
    password: string;
    loginStatus: string;

    data() {
        return {
            login: "",
            password: "",
            loginStatus: " ",
        }
    }


    loginAction(event: any) {
        if (event) event.preventDefault();
        //alert(this.login + " " + this.password);
        //alert(JSON.stringify({ login: this.login, password: this.password }));
        fetch('/api/Login', {
            credentials: 'include', 
            method: 'post',
            body: JSON.stringify({ login: this.login, password: this.password }),
            headers: new Headers({
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            })
        }).then((response) => {
            return response.text();
            }).then((text) => {
                var textSplitted: string[] = text.split(":");
                if (textSplitted.length == 2) {
                    (<any>Vue).notify(text, 0);
                } else {
                    /**
                     * Login successful.
                     */
                    (<any>Vue).notify(textSplitted[3], 0);
                    this.$store.commit("setDateByInput", (<any>Vue).toDateInputValue(new Date()));
                    (<any>Vue).redirectBack();
                }
                
                this.loginStatus = text;
        });
        
    }
    
}
