<script setup>
import { ref } from "vue";
import { useUserStore } from "../../store/user";


const user = useUserStore();
const {login} = user;


const email = ref(null);
const password = ref(null);

const loginin = () =>{
   login(email.value,password.value)
}

</script>

<template>
  <div class="login">
    <h3>login</h3>
    <input type="email" id="email" placeholder="user email" v-model="email" />
    <input
      type="password"
      id="password"
      placeholder="password"
      v-model="password"
    />
    <button @click="loginin">
      <span class="material-symbols-outlined"> arrow_forward </span>
    </button>
  </div>
</template>

<style scoped>
.login {
  width: 100%;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 5px;
}

.login h3 {
  color: var(--color5);
}

.login button {
  height: 30px;
  width: 90%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
}
.login button:focus {
  color: var(--color4);
  background-color: var(--color2);
}

.login input {
  height: 25px;
  width: 90%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
  border-bottom: 5px solid var(--color2);
}

.login input:focus {
  color: var(--color3);
  outline: none;
  border: 5px solid var(--color3);
  border-radius: 5px;
}
</style>
