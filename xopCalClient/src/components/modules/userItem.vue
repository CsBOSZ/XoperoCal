<script setup>
import { ref } from "vue";
import userStatusModal from "../userStatusModal.vue";
const props = defineProps({
  userId: Number,
  userEmail: String,
  userName: String,
  userColor: String,
  userZ: {
    type: Number,
    default: 5,
  },
});

const show = ref(false);
const sendEvent = (val) => {
  show.value = val;
};
</script>

<template>
  <div class="userItem">
    <userStatusModal
      :userId="userId"
      :show="show"
      :z="userZ"
      @close-modal="sendEvent"
    />
    <span
      class="coloruser"
      :style="{ 'background-color': userColor, color: userColor }"
      ><p>{{ userId }}</p></span
    >
    <p>{{ userName }}</p>
    <p>{{ userEmail }}</p>
    <span class="material-symbols-outlined" @click="show = true">
      visibility
    </span>
  </div>
</template>

<style scoped>
div.userItem {
  width: 100%;
  height: 100%;

  display: flex;
  justify-content: space-around;
  align-items: center;
  flex-wrap: wrap;
  font-weight: 800;

  color: var(--color5);
}
div.userItem > span.coloruser {
    width: 60px;
    aspect-ratio: 1/1;

    display: flex;
  justify-content: center;
  align-items: center;
}
div.userItem > span.coloruser > * {
  filter: invert(100%);
}

div.userItem > span.coloruser{
    border-radius: 30px;
}
</style>
