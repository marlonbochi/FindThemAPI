
<template>
    <div id="client">
        <header-menu ref="headerMenu" />
        <div class="container">
            <h2>Clientes {{title}}</h2>

            <div class="row" v-if="mode == 'list'">
                <div class="col-12">
                    <div class="row firstRow">
                        <div class="col-12 text-right">
                            <button  v-if="kindUser != 'user'" class="btn btn-primary" @click="add">Adicionar</button>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12">
                            <table class="table">
                                <thead class="thead-dark">
                                    <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Nome</th>
                                    <th scope="col">Email</th>
                                    <th scope="col">Avaliações</th>
                                    <th scope="col">Cadastrado</th>
                                    <th scope="col">Ações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="row in rows" v-bind:key="row.id">
                                        <th scope="row">{{row.id}}</th>
                                        <td>{{row.user.name}}</td>
                                        <td>{{row.user.email}}</td>
                                        <td>{{row.rateAVG ? row.rateAVG : 5}}/5</td>
                                        <td>{{row.dateInserted | dateFormat}}</td>
                                        <td>
                                            <button class="btn btn-sm btn-warning btn-margin-right" @click="edit(row.id)"><i class="fa fa-edit" aria-hidden="true"></i></button>
                                            <button class="btn btn-sm btn-danger" v-if="kindUser != 'user'" @click="remove(row.id)"><i class="fa fa-trash" aria-hidden="true"></i></button>
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
                                    <input type="text" class="form-control" required v-model="model.user.name" :disabled="mode == 'remove'">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Email *</label>
                                    <input type="email" class="form-control" required v-model="model.user.email" :disabled="mode == 'remove'" autocomplete="off">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Senha</label>
                                    <input type="password" class="form-control" v-model="model.user.password" v-if="mode == 'edit'" placeholder="Se preencher, será alterado a sua senha" autocomplete="off">
                                    <input type="password" class="form-control" v-model="model.user.password" v-else :disabled="mode == 'remove'" autocomplete="off">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Tipo de acesso *</label>
                                    <select class="form-control" v-if="kindUser != 'user'" required v-model="model.user.kind" :disabled="mode == 'remove'" >
                                        <option value="">Selecione um tipo</option>
                                        <option value="user">Usuário</option>
                                        <option value="admin">Administrador</option>
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
    import Client from "../store/Client";

    export default {
        name: 'client',
        components: {
            "headerMenu": HeaderMenu
        },
        data() {
            return {
                message: '',
                services: new Services(),
                mode: "list",
                title: "",
                model: new Client(),
                api: new API(),
                rows: [],
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
                    self.api.edit("/api/client/edit" + self.model.id, self.model).then(function(data) {
                        if (data.success) {
                            self.mode = "list";
                            self.findAll();
                            self.message = "";
                        } else {
                            self.message = data.message;
                        }
                    });
                } else if (self.mode == 'remove') {
                    self.api.remove("/api/client/delete", self.model.id).then(function(data) {
                        if (data.success) {
                            self.mode = "list";
                            self.findAll();
                            self.message = "";
                        } else {
                            self.message = data.message;
                        }
                    });
                } else if (self.mode == 'add') {
                    self.api.add("/api/client/create", self.model).then(function(data) {

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

                self.model = new Client();
            },

            edit: function($id) {
                var self = this;

                self.title = " > Editar"
                
                self.api.get("/api/client", $id).then(function(row) {

                    self.model = row;

                    self.mode = "edit";
                });
                
            },

            remove: function($id) {
                var self = this;

                self.title = " > Remover"
                
                self.api.get("/api/client", $id).then(function(row) {

                    self.model = row;

                    self.mode = "remove";
                });
            },

            cancel: function() {
                var self = this;

                self.mode = "list";

                self.title = ""

                self.model = new Object();

                self.findAll();
            },

            findAll: function() {
                var self = this;
                
                self.api.findAll("/api/client/FindAll").then(function(rows) {
                    self.rows = rows;
                });
            }
        }
    };
</script>