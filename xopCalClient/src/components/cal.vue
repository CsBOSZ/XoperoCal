<script setup>
import { ref ,watch } from "vue";
import eventItem from "./modules/eventItem.vue";
import { useUserStore } from "../store/user";
import { storeToRefs } from "pinia";

const user = useUserStore();
const { ht, jwt, id , se , ss} = storeToRefs(user);

var myHeaders;

const hf = () => {
  myHeaders = new Headers();
  myHeaders.append("Accept", "text/plain");
  myHeaders.append("Content-Type", "application/json");
  myHeaders.append("Authorization", "Bearer " + jwt.value);
};
hf();

const eventList = ref(null);
const start = ref(null);
const end = ref(null);
const name = ref(null);

const allf = () => {
  var requestOptions = {
    method: "GET",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(ht.value + "/Event/all", requestOptions)
    .then((response) => response.json())
    .then((result) => {
      eventList.value = result;
    })
    .catch((error) => console.log("error", error));
};

const myf = () => {
  hf();
  var requestOptions = {
    method: "GET",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(ht.value + "/Event/my", requestOptions)
    .then((response) => response.json())
    .then((result) => {
      eventList.value = result;
    })
    .catch((error) => console.log("error", error));
};

const namef = () => {
  var requestOptions = {
    method: "GET",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(ht.value + "/Event/eventName/" + name.value, requestOptions)
    .then((response) => response.json())
    .then((result) => {
      eventList.value = result;
    })
    .catch((error) => console.log("error", error));
};

const datef = () => {
  var requestOptions = {
    method: "GET",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(
    end.value
      ? ht.value +
          "/Event/time?StartEvent=" +
          start.value +
          "&EndEvent=" +
          end.value
      : ht.value + "/Event/time?StartEvent=" + start.value,
    requestOptions
  )
    .then((response) => response.json())
    .then((result) => {
      eventList.value = result;
    })
    .catch((error) => console.log("error", error));
};
watch( [ss,se] ,([newss,newse])=>{

allf();

});

</script>

<template>
  <div class="cal">
    <header class="hed">
      <span>
        <input type="datetime-local" v-model="start" />
        <input type="datetime-local" v-model="end" />
        <button @click="datef">
          <span class="material-symbols-outlined"> search </span>
        </button>
      </span>
      <span>
        <input type="text" placeholder="name" v-model="name" />
        <button @click="namef">
          <span class="material-symbols-outlined"> search </span>
        </button>
      </span>
      <span>
        <button>
          <span class="material-symbols-outlined" @click="allf">
            format_align_justify </span
          >All
        </button>
        <button v-if="id && jwt" @click="myf">
          <span class="material-symbols-outlined"> person </span>My
        </button>
      </span>
    </header>
    <div class="list">
      <eventItem
        v-for="item in eventList"
        :key="item.id"
        :event-color="item.color"
        :event-id="item.id"
        :event-start="item.startEvent"
        :event-name="item.name"
      />
    </div>
  </div>
</template>

<style scoped>
div.cal {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-direction: column;

  height: 100%;
  width: 100%;
}

div.cal header.hed {
  display: flex;
  align-items: center;
  justify-content: space-evenly;

  height: 50px;
  width: 100%;

  background-color: var(--color1);
}

div.cal header.hed > span {
  display: flex;
  align-items: center;
  width: 30%;
}

div.cal header.hed input {
  height: 25px;
  width: 90%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
  border-bottom: 5px solid var(--color2);
}

div.cal header.hed input:focus {
  color: var(--color3);
  outline: none;
  border: 5px solid var(--color3);
  border-radius: 5px;
}
div.cal header.hed button {
  height: 30px;
  width: 20%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
}
div.cal header.hed button:focus {
  color: var(--color4);
  background-color: var(--color2);
}

div.list {
  width: 90%;

  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}
</style>
