<script setup>
import notification from "./components/modules/notification.vue";
import bar from "./components/bar.vue";
import topBar from "./components/topBar.vue";
import { storeToRefs } from "pinia";
import { useUserStore } from "./store/user";
import { watch } from "vue";

const user = useUserStore();

const { showNotificationG, stringNotificationG, jwt, id, ht } =
  storeToRefs(user);

function getDaysInMonth(month, year) {
  var date = new Date(year, month, 1);
  var days = [];
  while (date.getMonth() === month) {
    days.push(new Date(date));
    date.setDate(date.getDate() + 1);
  }
  return days;
}
// watch(jwt, (newjwt , oldjwt) => {

// })

// "Authorization", "Bearer " + jwt.value

const connection = new signalR.HubConnectionBuilder()
  .withUrl(ht.value + "/EventHub", options =>
    { 
        options.AccessTokenProvider = () => Task.FromResult("Bearer " + jwt.value);
    })
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

connection.on("test", () => {
  console.log("test");
});
</script>

<template>
  <main class="ma">
    <teleport to="body">
      <notification :not="stringNotificationG" :show="showNotificationG" />
    </teleport>
    <topBar />
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
</style>
