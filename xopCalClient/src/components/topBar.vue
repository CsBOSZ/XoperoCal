<script setup>
import notification from "./modules/notification.vue";
import { ref } from "vue";
import { useUserStore } from "../store/user";
import { storeToRefs } from "pinia";

const user = useUserStore();
const { ht, jwt, id } = storeToRefs(user);

const start = ref(null);
const end = ref(null);
const namei = ref(null);
const description = ref(null);
const colori = ref(null);
const addnot = ref(null);
const addshownot = ref(false);
const uploadfile = ref(false);
const raw = ref(null);
var myHeaders;

const hf = ( ) =>{
  myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");
  myHeaders.append("Authorization", "Bearer " + jwt.value);
}
const send = () => {
 hf();

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw.value,
    redirect: "follow",
  };

  fetch(ht.value + "/Event", requestOptions)
    .then((response) => {
      addshownot.value = Math.floor(+response.status / 100) != 2;
      return response.json();
    })
    .then((result) => {
      addnot.value = result;
    })
    .catch((error) => {
      addnot.value = error;
    });
};

const upload = (e) => {
  const reader = new FileReader();
  reader.onload = function () {
    uploadfile.value = JSON.parse(reader.result);
  };
  reader.readAsText(e.target.files[0]);
};

const uploadSend = () =>{
  raw.value = JSON.stringify({
    name: uploadfile.value.name,
    description: uploadfile.value.description,
    startEvent: uploadfile.value.startEvent,
    endEvent: uploadfile.value.endEvent,
    color: uploadfile.value.color,
  });
  send();
}

const manSend = () =>{
  raw.value = JSON.stringify({
    name: namei.value,
    description: description.value,
    startEvent: start.value,
    endEvent: end.value,
    color: colori.value,
  });
  send();
}

const tak = ref(null);
</script>

<template>
  <main class="top">
    <section class="add upload" v-if="jwt != null && jwt != '' && id != null">
      <span class="addD">
        <input type="file" @change="upload" />
        <span @click="uploadSend" class="material-symbols-outlined"> upload_file </span>
        </span
      >
      {{ uploadfile }}
    </section>
    <section class="add" v-if="jwt != null && jwt != '' && id != null">
      <notification :not="addnot" :show="addshownot" />
      <input type="datetime-local" v-model="start" />
      <input type="datetime-local" v-model="end" />
      <span class="addD">
        <input type="text" placeholder="name" v-model="namei" />
        <input type="color" v-model="colori" />
      </span>
      <input type="text" placeholder="description" v-model="description" />
      <button @click="manSend">
        <span class="material-symbols-outlined"> add </span>
      </button>
    </section>
  </main>
</template>

<style scoped>
main.top {
  display: flex;
  justify-content: space-between;
  align-items: center;

  width: 100%;
  height: 200px;

  background-color: var(--color5);
}
main.top section.add {
  position: relative;

  width: 45%;
  height: 80%;

  display: flex;
  justify-content: space-evenly;
  align-items: center;
  flex-direction: column;

  background-color: var(--color4);
  border-radius: 15px 0 0 15px;
}
main.top section.upload {
  border-radius: 0 15px 15px 0;
}

main.top section.add span.addD {
  width: 90%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

main.top section.add input {
  height: 25px;
  width: 90%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
  border-bottom: 5px solid var(--color2);
}

main.top section.add input:focus {
  color: var(--color3);
  outline: none;
  border: 5px solid var(--color3);
  border-radius: 5px;
}
main.top section.add button {
  height: 30px;
  width: 90%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
}
main.top section.add button:focus {
  color: var(--color4);
  background-color: var(--color2);
}
</style>
