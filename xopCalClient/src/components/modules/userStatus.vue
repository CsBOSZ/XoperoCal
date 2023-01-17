<script setup>
import { ref } from "vue";
import { useUserStore } from "../../store/user";
import { storeToRefs } from "pinia";
import userStatusModal from "../userStatusModal.vue";

const user = useUserStore();
const { id, name, email , color} = storeToRefs(user);

const show = ref(false);

const sendEvent = val => {
  show.value = val;
}
</script>

<template>
   <userStatusModal :userId="id" :show="show" :z="5" @close-modal="sendEvent"/>
  <div class="user" @click="show = true">
    <h3>{{ name }}</h3>
    <span class="color" :style="{'background-color':color}">{{ name }}</span>
    <h4>{{ email }}</h4>
    <h2>ID:{{ id }}</h2>
  </div>
  

</template>

<style scoped>
.user {
  width: 100%;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 5px;
  cursor: pointer;
}

.user h3,.user h4 , .user h2{
  color: var(--color5);
  margin: 10px;
}

.color {
  position: relative;

  display: flex;
  justify-content: center;
  align-items: center;

  background-color: var(--color4);
  width: 100px;
  aspect-ratio: 1/1;
  border-radius: 50px;
  font-weight: 900;
}

.color::before {
  position: absolute;

  content: "";
  width: 80px;
  aspect-ratio: 1/1;
  background-color: #0000;
  border-radius: 40px;
  top: -10px;
  right: -10px;
  backdrop-filter: invert(100%);
}
</style>
