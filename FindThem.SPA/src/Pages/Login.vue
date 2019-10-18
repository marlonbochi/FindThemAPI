
<template>
    <div class="login_container">
        <div class="content text-center">
            <form class="form-signin" v-on:submit.prevent="signIn()">
                <h1 class="h3 mb-3 font-weight-normal">Digite seus dados para entrar</h1>
                <label for="inputEmail" class="sr-only">Email</label>
                <input type="email" id="inputEmail" class="form-control" placeholder="Email" v-model="username" required="" autofocus="">
                <label for="inputPassword" class="sr-only">Senha</label>
                <input type="password" id="inputPassword" class="form-control" placeholder="Senha" v-model="password" required="">
                <div class="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="remember-me" v-model="rememberMe"> Lembrar-me
                    </label>
                </div>
                <button class="btn btn-lg btn-primary btn-block" type="submit">Entrar</button>
                <div class="alert alert-warning" role="alert" v-if="message != ''">
                    {{message}}
                </div>
            </form>
        </div>
    </div>
</template>
<script>
    import axios from "axios";
    import Services from "../Services/mainServices";

    export default {
        name: 'Login',
        data() {
            return {
                username: "",
                password: "",
                message: "",
                services: new Services(),
                rememberMe: false
            };
        },
        methods: {
            signIn: function () {
                var self = this;

                let data = new FormData();

                data.append('login', self.username);
                data.append('password', self.password);

                axios.post(sessionStorage.getItem("urlAPI") + '/api/login/signin',
                    data
                ).then(function (response) {

                    const data = response.data;

                    if (data.message) {
                        self.message = data.message;
                        return false;
                    }
                    self.message = "";

                    if (self.rememberMe) {
                        self.services.setCookie("tokenID", data.token, 7);
                        self.services.setCookie("tokenExpiration", data.expiration, 7);
                    }

                    sessionStorage.setItem('tokenID', data.token);
                    sessionStorage.setItem('tokenExpiration', data.expiration);

                    self.$router.push("/");
                    
                }).catch(function (error) {
                    self.message = error.message;
                });
            }
        }
    };
</script>