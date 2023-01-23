<script setup>
import { ref , watch} from "vue";
import modal from "./modules/modal.vue";
import notification from "./modules/notification.vue";
import eventItem from "./modules/eventItem.vue";
import { useUserStore } from "../store/user";
import { storeToRefs } from "pinia";

const user = useUserStore();
const { ht , ss , se} = storeToRefs(user);

const props = defineProps({
  userId: {
    type: Number,
    default: 0,
  },
  show: Boolean,
  z: {
    type: Number,
    default: 5,
  },
});

const name = ref(null);
const email = ref(null);
const color = ref(null);
const eventCals = ref(null);
const subscribeEventCals = ref(null);
const showNotification = ref(null);

const emit = defineEmits(["closeModal"]);

const sendEvent = (val) => {
  props.show = val;
  emit("closeModal", val);
};

const userfetch = () => {

var myHeaders = new Headers();
myHeaders.append("Accept", "text/plain");
myHeaders.append("Content-Type", "application/json");

var requestOptions = {
  method: 'GET',
  headers: myHeaders,
  redirect: 'follow'
};

fetch(ht.value + "/User/true/" + props.userId, requestOptions, requestOptions)
  .then(response => {
      showNotification.value = (Math.floor(+response.status / 100) != 2);
      return response.json();
    })
  .then(result => {
      if (!showNotification.value) {

        name.value = result.name;
        email.value = result.email;
        color.value = result.color;
        eventCals.value = result.eventCals;
        subscribeEventCals.value = result.subscribeEventCals;
      }
    })
  .catch(error => console.log('error', error));
        
}
userfetch();

watch( [ss,se] ,([newss,newse])=>{

userfetch();

});

</script>

<template>
  <modal :show="show" :z="z" @close-modal="sendEvent">
    <notification not="id error" :show="showNotification" />
    <div class="user" @click="show = true">
      <h3>{{ name }}</h3>
      <span class="color" :style="{ 'background-color': color }">{{
        name
      }}</span>
      <h4>{{ email }}</h4>
      <h2>ID:{{ userId }}</h2>
      <span class="lists">

      <div class="list" >

        <eventItem v-for="item in eventCals" :key="item.id" :event-z="(z+1)" :event-color="item.color" :event-id="item.id" :event-start="item.startEvent"  :event-name="item.name" />
      
      </div>
      <div class="list">

        <eventItem v-for="item in subscribeEventCals" :key="item.id" :event-z="(z+1)" :event-color="item.color" :event-id="item.id" :event-start="item.startEvent"  :event-name="item.name" />

      </div>

    </span>
    </div>
    
  </modal>
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

.user h3,
.user h4,
.user h2 {
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

span.lists{
  width: 95%;

  display: flex;
  justify-content: center;
  align-items: center;
}

div.list{

  width: 50%;

  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;

}

</style>
