import Axios from "axios";

export default class Api {

    constructor() {
        this.config = { 
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("tokenID"),
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json'
            }
        };
    }

    async findAll ($url, $page) {

        if (!$page) {
            $page = 1
        }
        
        let data = await Axios.get(
            sessionStorage.getItem("urlAPI") + $url + "?page=" + $page, 
            this.config
        );

        return data.data
    }

    async get ($url, $id) {

        let data = await Axios.get(
            sessionStorage.getItem("urlAPI") + $url + "/" + $id, 
            this.config
        );

        return data.data
    }

    async add ($url, $model) {

        let data = await Axios.post(
            sessionStorage.getItem("urlAPI") + $url, 
            $model,
            this.config
        );

        return data.data
    }

    async edit ($url, $model) {

        let data = await Axios.post(
            sessionStorage.getItem("urlAPI") + $url, 
            $model,
            this.config
        );

        return data.data
    }

    async remove ($url, $id) {

        let data = await Axios.delete(
            sessionStorage.getItem("urlAPI") + $url + "/" + $id,
            this.config
        );

        return data.data
    }

    async getCep($cep) {
        let data = await Axios.get("https://viacep.com.br/ws/" + $cep + "/json/");

        return data.data
    }
}