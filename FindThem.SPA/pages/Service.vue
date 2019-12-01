
<template>
    <div id="client">
        <header-menu ref="headerMenu" />
        <div class="container">
            <h2>Serviços {{title}}</h2>

            <div class="row" v-if="mode == 'list'">
                <div class="col-12">
                    <div class="row firstRow">
                        <div class="col-12 text-right">
                            <button class="btn btn-primary" @click="add">Adicionar</button>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <table class="table">
                                <thead class="thead-dark">
                                    <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Nome</th>
                                    <th scope="col">Preço</th>
                                    <th scope="col">Prestador de Serviço</th>
                                    <th scope="col">Ações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="row in rows" v-bind:key="row.id">
                                        <th scope="row">{{row.id}}</th>
                                        <td>{{row.name}}</td>
                                        <td>{{row.price}}</td>
                                        <td>{{row.provider.name}}</td>
                                        <td>
                                            <button class="btn btn-sm btn-warning btn-margin-right" @click="edit(row.id)"><i class="fa fa-edit" aria-hidden="true"></i></button>
                                            <button class="btn btn-sm btn-danger" @click="remove(row.id)"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                        </td>
                                    </tr>                                    
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" v-if="mode != 'list'">
                <div class="col-12">
                    <form v-on:submit.prevent="sendForm">
                        <div class="row">                            
                            <div class="col-12">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Nome *</label>
                                    <input type="text" class="form-control" required v-model="model.name" :disabled="mode == 'remove'">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Descrição</label>
                                    <input type="text" class="form-control" v-model="model.description" :disabled="mode == 'remove'">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Materiais</label>
                                    <input type="text" class="form-control" v-model="model.materials" :disabled="mode == 'remove'">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Preço</label>
                                    <input type="number" class="form-control" v-model="model.price" :disabled="mode == 'remove'">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Tempo de execução (em horas)</label>
                                    <input type="number" class="form-control" v-model="model.timeExecution" :disabled="mode == 'remove'">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Prestador de serviço</label>
                                    <select class="form-control" v-model="model.provider.id" :disabled="mode == 'remove'">
                                        <option value="">Selecione um prestador</option>
                                        <option v-for="provider in providers" v-bind:key="provider.id" :value="provider.id">
                                            {{provider.name}}
                                        </option>
                                    </select>
                                </div>
                                <div class="row">
                                    <div class="col-6 text-left">
                                        <button type="button" class="btn btn-secondary" @click="cancel">Cancelar</button>
                                    </div>
                                    <div class="col-6 text-right" v-if="mode == 'remove'">
                                        <button type="submit" class="btn btn-danger">Excluir</button>
                                    </div>
                                    <div class="col-6 text-right" v-else>
                                        <button type="submit" class="btn btn-success">Salvar</button>
                                    </div>
                                </div>
                                <div class="alert alert-danger margin-top" role="alert" v-if="message != ''">
                                    {{message}}
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import Services from "../middleware/mainServices";
    import HeaderMenu from "../components/HeaderMenu.vue";
    import API from "../middleware/apiServices";
    import dateFormat from "../middleware/Date";
    import Service from "../store/Service";
    import Provider from "../store/Provider";

    export default {
        name: 'service',
        components: {
            "headerMenu": HeaderMenu
        },
        data() {
            return {
                message: '',
                services: new Services(),
                mode: "list",
                title: "",
                model: new Service(),
                api: new API(),
                rows: [],
                providers: [],
                kindUser: sessionStorage.getItem("kindUser")
            };
        },
        mounted: function() {
            this.findAll();
        },
        methods: {
            sendForm: function() {
                var self = this;

                if (self.mode == 'edit') {
                    self.api.edit("/api/service/edit" + self.model.id, self.model).then(function(data) {
                        if (data.success) {
                            self.mode = "list";
                            self.findAll();
                            self.message = "";
                        } else {
                            self.message = data.message;
                        }
                    });
                } else if (self.mode == 'remove') {
                    self.api.remove("/api/service/delete", self.model.id).then(function(data) {
                        if (data.success) {
                            self.mode = "list";
                            self.findAll();
                            self.message = "";
                        } else {
                            self.message = data.message;
                        }
                    });
                } else if (self.mode == 'add') {
                    self.api.add("/api/service/create", self.model).then(function(data) {

                        if (data.success) {
                            self.mode = "list";
                            self.findAll();
                            self.message = "";
                        } else {
                            self.message = data.message;
                        }
                    });
                }
            },

            add: function() {
                var self = this;
                self.mode = "add";

                self.title = " > Adicionar"

                self.model = new Service();

                self.api.findAll("/api/provider/FindAll").then(function(rows) {
                    self.providers = rows;
                });
            },

            edit: function($id) {
                var self = this;

                self.title = " > Editar"
                
                self.api.get("/api/service", $id).then(function(row) {

                    self.model = row;

                    self.mode = "edit";
                });

                self.api.findAll("/api/provider/FindAll").then(function(rows) {
                    self.providers = rows;
                });
                
            },

            remove: function($id) {
                var self = this;

                self.title = " > Remover"
                
                self.api.get("/api/service", $id).then(function(row) {

                    self.model = row;

                    self.mode = "remove";
                });

                self.api.findAll("/api/provider/FindAll").then(function(rows) {
                    self.providers = rows;
                });
            },

            cancel: function() {
                var self = this;

                self.mode = "list";

                self.title = ""

                self.model = new Object();

                self.findAll();

                self.message = "";
            },

            findAll: function() {
                var self = this;
                self.api.findAll("/api/service/FindAll").then(function(rows) {
                    self.rows = rows;
                });
            }
        }
    };
</script>