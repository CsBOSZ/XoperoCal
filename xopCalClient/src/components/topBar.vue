<script setup>
import register from "./modules/register.vue";
import login from "./modules/login.vue";
import userStatus from "./modules/userStatus.vue";
import { ref } from "vue";
import { useUserStore } from "../store/user";
import { storeToRefs } from "pinia";

const user = useUserStore();
const { jwt, id } = storeToRefs(user);

const tak = ref(null);

</script>

<template>
  <main class="top">
    <section></section>
    <section class="add">

        <span class="addD"><p>start</p><p style="color: red;">*</p><input type="datetime-local" name="" id="" v-model="tak"></span>
        <span class="addD"><p>end</p><input type="datetime-local" name="" id="" v-model="tak"></span>

           
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
  width: 45%;
  height: 80%;

  display: flex;
  justify-content: space-evenly;
  align-items: center;
  flex-direction: column;

  background-color: var(--color4);
  border-radius: 15px 0 0 15px;
}

main.top section.add span.addD{
    display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
