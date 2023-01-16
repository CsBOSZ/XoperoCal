<script setup>
import notification from './components/modules/notification.vue';
import bar from './components/bar.vue';
import { storeToRefs } from 'pinia';
import { useUserStore } from './store/user';

const user = useUserStore();

const { showNotificationG , stringNotificationG } = storeToRefs(user);


</script>

<template>
<teleport to='body' >
  <notification :not="stringNotificationG" :show="showNotificationG"/>
</teleport>


 <bar/>

</template>

<style scoped>



</style>
