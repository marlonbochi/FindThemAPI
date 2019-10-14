
<template>
    <div class="login_container">
        <div class="content text-center">
            <form class="form-signin">
                <h1 class="h3 mb-3 font-weight-normal">Digite seus dados para entrar</h1>
                <label for="inputEmail" class="sr-only">Email</label>
                <input type="email" id="inputEmail" class="form-control" placeholder="Email" required="" autofocus="">
                <label for="inputPassword" class="sr-only">Senha</label>
                <input type="password" id="inputPassword" class="form-control" placeholder="Senha" required="">
                <div class="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="remember-me"> Lembrar-me
                    </label>
                </div>
                <button class="btn btn-lg btn-primary btn-block" type="submit">Entrar</button>
            </form>
        </div>
    </div>
</template>
<script>
    export default {
        name: 'Login',
        data() {
            return {
                message: 'Data from Login'
            };
        }        
    };
</script>