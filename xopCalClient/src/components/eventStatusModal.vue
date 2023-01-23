<script setup>
import { ref , watch } from "vue";
import modal from "./modules/modal.vue";
import notification from "./modules/notification.vue";
import userItem from "./modules/userItem.vue";
import { saveAs } from "file-saver";
import { useUserStore } from "../store/user";
import { storeToRefs } from "pinia";

const user = useUserStore();
const { ht, jwt, id , se , ss } = storeToRefs(user);

const props = defineProps({
  eventId: {
    type: Number,
    default: 0,
  },
  show: Boolean,
  z: {
    type: Number,
    default: 5,
  },
});

const eventStatus = ref(0);
const eventStart = ref(null);
const eventEnd = ref(null);
const eventName = ref(null);
const eventColor = ref(null);
const showNotification = ref(null);
const eventDescription = ref(null);
const ownerId = ref(null);
const ownerName = ref(null);
const ownerEmail = ref(null);
const subscribers = ref(null);
const filejson = ref(null);
const ed = ref(false);

const start = ref(null);
const end = ref(null);
const namei = ref(null);
const description = ref(null);
const colori = ref(null);
const addnot = ref(null);
const addshownot = ref(false);

const emit = defineEmits(["closeModal"]);

const sendEvent = (val) => {
  props.show = val;
  emit("closeModal", val);
};

var myHeaders;

const hf = () => {
  myHeaders = new Headers();
  myHeaders.append("Accept", "text/plain");
  myHeaders.append("Content-Type", "application/json");
  myHeaders.append("Authorization", "Bearer " + jwt.value);
};

const eventfetch = () => {
  hf();

  var requestOptions = {
    method: "GET",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(ht.value + "/Event/Status/" + props.eventId, requestOptions)
    .then((response) => {
      showNotification.value = Math.floor(+response.status / 100) != 2;
      return response.text();
    })
    .then((result) => {
      if (!showNotification.value) {
        eventStatus.value = +result;
      }
    })
    .catch((error) => console.log("error", error));

  fetch(ht.value + "/Event/" + props.eventId, requestOptions)
    .then((response) => {
      showNotification.value = Math.floor(+response.status / 100) != 2;
      return response.json();
    })
    .then((result) => {
      if (!showNotification.value) {
        filejson.value = result;
        eventStart.value = result.startEvent;
        eventEnd.value = result.endEvent;
        eventColor.value = result.color;
        eventName.value = result.name;
        eventDescription.value = result.description;
        ownerId.value = result.ownerId;
        ownerName.value = result.ownerName;
        ownerEmail.value = result.ownerEmail;
        subscribers.value = result.subscribers;
      }
    })
    .catch((error) => console.log("error", error));
};
eventfetch();

const eventdelete = () => {
  var requestOptions = {
    method: "DELETE",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(ht.value + "/Event/" + props.eventId, requestOptions)
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
};

const eventSubscribe = () => {
  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(ht.value + "/Event/Subscribe/" + props.eventId, requestOptions)
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
};

const eventUnSubscribe = () => {
  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    redirect: "follow",
  };

  fetch(ht.value + "/Event/UnSubscribe/" + props.eventId, requestOptions)
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
};

const dojson = () => {
  const json = JSON.stringify(filejson.value);
  const blob = new Blob([json], { type: "application/json;charset=utf-8" });
  saveAs(blob, "event.json");
};

const editf = () => {
  hf();

  var raw = JSON.stringify({
    id: props.eventId,
    name: namei.value,
    description: description.value,
    startEvent: start.value,
    endEvent: end.value,
    color: colori.value,
  });

  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  fetch(ht.value + "/Event", requestOptions)
    .then((response) => {
      addshownot.value = Math.floor(+response.status / 100) != 2;
      return response.json();
    })
    .then((result) => {
      addnot.value = result;
    })
    .catch((error) => {
      addnot.value = result;
    });
};

watch( [ss,se] ,([newss,newse])=>{

eventfetch();

});

</script>

<template>
  <modal :show="show" :z="z" @close-modal="sendEvent">
    <notification not="error" :show="showNotification" />
    <span class="e">
      <div
        class="event"
        :style="{ 'background-color': eventColor, color: eventColor }"
      >
        <span @click="dojson" class="material-symbols-outlined tojsonfile">
          download
        </span>
        <div class="edit" v-if="ed">
          <section class="add" v-if="jwt != null && jwt != '' && id != null">
            <notification :not="addnot" :show="addshownot" />
            <input type="datetime-local" v-model="start" />
            <input type="datetime-local" v-model="end" />
            <span class="addD">
              <input type="text" placeholder="name" v-model="namei" />
              <input type="color" v-model="colori" />
            </span>
            <input
              type="text"
              placeholder="description"
              v-model="description"
            />
            <button @click="editf">
              <span class="material-symbols-outlined"> save_as </span>
            </button>
          </section>
        </div>
        <p>{{ eventName }}</p>
        <p>{{ eventDescription }}</p>

        <p>{{ eventStart }} => {{ eventEnd }}</p>

        <p>
          ownerId={{ ownerId }} | ownerName={{ ownerName }} | ownerEmail={{
            ownerEmail
          }}
        </p>
        <span v-if="eventStatus == 1">
          <span class="material-symbols-outlined" @click="ed = !ed">
            edit
          </span>
          <span class="material-symbols-outlined" @click="eventdelete">
            delete_forever
          </span>
        </span>
        <span
          class="material-symbols-outlined"
          @click="eventUnSubscribe"
          v-else-if="eventStatus == 2"
        >
          heart_minus
        </span>
        <span class="material-symbols-outlined" @click="eventSubscribe" v-else>
          heart_plus
        </span>
      </div>
      <userItem
        :user-z="z + 1"
        v-for="item in subscribers"
        :key="item.id"
        :user-color="item.color"
        :user-id="item.id"
        :user-email="item.email"
        :user-name="item.name"
      />
    </span>
  </modal>
</template>

<style scoped>
span.e {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}
div.event {
  position: relative;

  display: flex;
  justify-content: space-evenly;
  align-items: center;
  flex-direction: column;

  color: var(--color5);
  border: solid 2px var(--color1);
}
div.event > * {
  filter: invert(100%);
}
div.event span.material-symbols-outlined {
  cursor: pointer;
  font-size: 2rem;
}

div.event span.material-symbols-outlined.tojsonfile {
  position: absolute;
  top: 0;
  left: 0;
}

div.event > div.edit {
  display: flex;
  justify-content: end;
  align-items: center;

  width: 100%;
  height: 300px;

  filter: invert(0%);
}

div.event > div.edit section.add {
  position: relative;

  width: 100%;
  height: 100%;

  display: flex;
  justify-content: space-evenly;
  align-items: center;
  flex-direction: column;

  background-color: var(--color1);
  border-radius: 30% 0 15px 15px;
}

div.event > div.edit section.add span.addD {
  width: 80%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

div.event > div.edit section.add input {
  height: 25px;
  width: 80%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
  border-bottom: 5px solid var(--color2);
}

div.event > div.edit section.add input:focus {
  color: var(--color3);
  outline: none;
  border: 5px solid var(--color3);
  border-radius: 5px;
}
div.event > div.edit section.add button {
  height: 30px;
  width: 80%;
  background-color: var(--color4);
  color: var(--color2);
  border: none;
}
div.event > div.edit section.add button:focus {
  color: var(--color4);
  background-color: var(--color2);
}
</style>
