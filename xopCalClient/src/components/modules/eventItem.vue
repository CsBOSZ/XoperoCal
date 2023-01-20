<script setup>
import { ref } from 'vue';
import eventStatusModal from '../eventStatusModal.vue';
const props = defineProps({
  eventId: Number,
  eventStart: String,
  eventName: String,
  eventColor: String,
  eventZ: {
    type: Number,
    default: 5,
  },
});

const show = ref(false);
const sendEvent = val => {
  show.value = val;
}
</script>

<template>
  <div
    class="eventItem"
    :style="{ 'background-color': eventColor, 'color': eventColor }"
  >
    <eventStatusModal :eventId="eventId" :show="show" :z="eventZ" @close-modal="sendEvent"/>
    <p>{{ eventId }}</p>
    <p>{{ eventName }}</p>
    <p>{{ eventStart }}</p>
    <span class="material-symbols-outlined" @click="show = true"> visibility </span>
    
  </div>
</template>

<style scoped>
div.eventItem {
  width: 100%;
  height: 100%;

  display: flex;
  justify-content: space-around;
  align-items: center;
  flex-wrap: wrap;
  font-weight: 800;
}
div.eventItem > * {
  filter: invert(100%);
}
</style>
