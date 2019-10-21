import Vue from "vue";
import moment from "moment";

Vue.filter('dateFormat', function (value) {
    moment.locale('pt-BR');

    if (!value) return moment(value);

    value = value.toString()

    return moment(value).format("DD/MM/YYYY HH:mm");
});