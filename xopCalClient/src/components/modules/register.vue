<script setup>
import { ref } from "vue";
import notification from "./notification.vue";
import { useUserStore } from "../../store/user";
import { storeToRefs } from 'pinia';

const user = useUserStore();
const { ht } = storeToRefs(user);

const email = ref(null);
const name = ref(null);
const color = ref(null);
const password = ref(null);
const passwordC = ref(null);
const stringNotification = ref(null);
const showNotification = ref(false);

const register = () => {
  let myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");

  let raw = JSON.stringify({
    email: email.value,
    name: name.value,
    password: password.value,
    confirmPassword: passwordC.value,
    color: color.value
  });

  let requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  fetch(ht.value+"/Auth/register", requestOptions)
    .then((response) => {
      showNotification.value = (Math.floor(+response.status / 100) != 2); 
      return response.json();
    })
    .then((result) => {
        stringNotification.value = result;
    });
  // .catch((error) => {stringNotification.value = error; showNotification.value = true;});
};
</script>

<template>
  <teleport to="body">
    <notification :not="stringNotification" :show="showNotification" />
  </teleport>
  <div class="register">
    <h3>REGISTER</h3>
    <input type="email" id="email" placeholder="user email" v-model="email" />
    <input type="text" id="name" placeholder="user name" v-model="name" />
    <input type="color" id="color" v-model="color"/>
    <input
      type="password"
      id="password"
      placeholder="password"
      v-model="password"
    />
    <input
      type="password"
      id="passwordC"
      placeholder="password confirmation"
      v-model="passwordC"
    />
    <button @click="register">
      <span class="material-symbols-outlined"> arrow_forward </span>
    </button>
  </div>
</template>

<style scoped>
.register {
  width: 100%;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 5px;
}

.register h3 {
  color: var(--color5);
}

.register button {
  height: 30px;
  width: 90%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
}
.register button:focus {
  color: var(--color4);
  background-color: var(--color2);
}

.register input {
  height: 25px;
  width: 90%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
  border-bottom: 5px solid var(--color2);
}

.register input:focus {
  color: var(--color3);
  outline: none;
  border: 5px solid var(--color3);
  border-radius: 5px;
}
</style>
