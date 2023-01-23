<script setup>
import notification from "./components/modules/notification.vue";
import bar from "./components/bar.vue";
import topBar from "./components/topBar.vue";
import cal from "./components/cal.vue";
import { storeToRefs } from "pinia";
import { useUserStore } from "./store/user";
import { ref , watch } from "vue";

const user = useUserStore();

const { showNotificationG, stringNotificationG, jwt, id, ht , ss , se} =
  storeToRefs(user);

  const showwatchevent = new ref(false);
  const idwatchevent = new ref(null);
  const namewatchevent = new ref(null);

function getDaysInMonth(month, year) {
  var date = new Date(year, month, 1);
  var days = [];
  while (date.getMonth() === month) {
    days.push(new Date(date));
    date.setDate(date.getDate() + 1);
  }
  return days;
}
watch(jwt, (newjwt , oldjwt) => {

// "Authorization", "Bearer " + jwt.value

const connection = new signalR.HubConnectionBuilder()
  .withUrl(ht.value + "/EventHub", { accessTokenFactory: () => newjwt})
  .build();


async function start() {
  try {
    await connection.start();
    console.log("SignalR Connected.");
  } catch (err) {
    console.log(err);
    setTimeout(start, 5000);
  }
}

connection.onclose(async () => {
  await start();
});

// Start the connection.
start();

connection.on("newevent", () => {
  console.log("newevent");
  alert("newevent")
  se.value++;
});
connection.on("Subscribe", (en,un) => {
  console.log("Subscribe event-name:"+en+" user-name:"+un);
  alert("Subscribe event-name:"+en+" user-name:"+un)
  ss.value++;
});
connection.on("UnSubscribe", (en,un) => {
  console.log("UnSubscribe event-name:"+en+" user-name:"+un);
  alert("UnSubscribe event-name:"+en+" user-name:"+un)
  ss.value++;
});

connection.on("watch", (eid,en) => {

  namewatchevent.value = en;
  idwatchevent.value = +eid;
  showwatchevent.value = true;

});

})

const snoozef = () =>{
showwatchevent.value = false; 
  var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer "+jwt.value);

var requestOptions = {
  method: 'PUT',
  headers: myHeaders,
  redirect: 'follow'
};

fetch(ht.value+"/Event/Snooze/"+ idwatchevent.value, requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
}


</script>

<template>
  <main class="ma">
    <teleport to="body">
      <span class="watchevent" v-show="showwatchevent">
          <h2>{{ namewatchevent }}</h2>
          <span><button @click="snoozef">snooze</button><button @click="showwatchevent = false" >confirm</button></span>
          
      </span>
      <notification :not="stringNotificationG" :show="showNotificationG" />
    </teleport>
    <topBar />
    <cal />
    <bar />
  </main>
</template>

<style scoped>
main.ma {
  height: 100%;
  width: 100%;

  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.watchevent{

position: absolute;
top: 0;
left: 50%;
transform: translateX(-50%);

width: 60%;
height: 300px;

color: var(--color5);
background-color: var(--color1);
border-radius: 0 0 25px 25px;
border: 3px solid var(--color3);
border-top:10px solid var(--color2) ;

display: flex;
justify-content: space-evenly;
align-items: center;
flex-direction: column;

}
</style>
