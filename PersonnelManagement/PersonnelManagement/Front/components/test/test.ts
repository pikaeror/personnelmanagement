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

class Person {
    name: string;
    surname: string;
    dateOfBirth: Date;
    id: string;

}



@Component
export default class TestComponent extends Vue {
    persons: Person[];
    newPersonName: string;
    newPersonSurname: string;
    newPersonDate: Date;
    access: string;


    data() {
        return {
            persons: [],
            
        }
    }

    mounted() {
        //this.fetchTest();
        this.fetchAccess().then(c => {
            this.fetchData();
            setInterval(this.fetchData, 2000);
            setInterval(this.fetchAccess, 3000);
        });
        
        //setInterval(this.fetchTest, 1500);
    }

    async fetchAccess() {
        this.access = await (<any>Vue).getAccessStatus();
        (<any>Vue).requireLogin(this.access);
    }

    fetchData() {
        fetch('api/Data', { credentials: 'include' })
            .then(response => response.json() as Promise<Person[]>)
            .then(data => {
                this.persons = data;
            })
    }

    addPerson(event: any) {
        if (event) event.preventDefault();
        var pattern = /(\d{2})\.(\d{2})\.(\d{4})/;
        var date = new Date(this.newPersonDate.toString().replace(pattern, '$3-$2-$1'));;
        fetch('/api/Data', {
            method: 'post',
            body: JSON.stringify(<Person>{ name: this.newPersonName, surname: this.newPersonSurname, dateOfBirth: date }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { response.json() as Promise<Person>; })
            .then((newPerson) => {
                this.fetchData();
            });
    }

    getDOB(person: Person) {
        return person.dateOfBirth.toString().slice(0,10);
    }
    //setInterval(function {this.})
}