<script setup>
import register from "./modules/register.vue";
import login from "./modules/login.vue";
import userStatus from "./modules/userStatus.vue";
import { ref } from "vue";
import { useUserStore } from "../store/user";
import { storeToRefs } from "pinia";

const user = useUserStore();
const { jwt } = storeToRefs(user);

const show = ref(true);
</script>

<template>
  <nav :class="{ hidden: !show }">
    <span class="open" @click="show = !show">
      <span class="material-symbols-outlined"> menu_open </span>
    </span>

    <userStatus v-if="jwt != null && jwt != ''" />
    <span class="rl" v-else>
      <register />
      <login />
    </span>
  </nav>
</template>

<style scoped>
nav {
  position: absolute;
  top: 0;
  left: 0;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  background-color: var(--color1);
  height: 100%;
  width: 200px;

  transition: 1s;
}
nav.hidden {
  transform: translateX(-100%);
}

.open {
  position: absolute;
  top: 0;
  right: 0;
  transform: translateX(100%);

  display: flex;
  justify-content: center;
  align-items: center;

  background-color: var(--color1);
  width: 50px;
  aspect-ratio: 1/1;
  border-radius: 0% 0% 50% 0%;
  color: var(--color4);
}
</style>
