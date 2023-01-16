<script setup>
import notification from './components/modules/notification.vue';
import bar from './components/bar.vue';
import { storeToRefs } from 'pinia';
import { useUserStore } from './store/user';

const user = useUserStore();

const { showNotificationG , stringNotificationG } = storeToRefs(user);

function getDaysInMonth(month, year) {
  var date = new Date(year, month, 1);
  var days = [];
  while (date.getMonth() === month) {
    days.push(new Date(date));
    date.setDate(date.getDate() + 1);
  }
  return days;
}

</script>

<template>
<teleport to='body' >
  <notification :not="stringNotificationG" :show="showNotificationG"/>
</teleport>


 <bar/>

</template>

<style scoped>



</style>
