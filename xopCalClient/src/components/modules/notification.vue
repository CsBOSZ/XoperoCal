<script setup>
defineProps({
  not: String,
  show: Boolean,
});
</script>

<template>
  <div class="notification" v-if="show">
    <span>
      <p>{{ not }}</p>
      <button @click="show = false">ok</button>
    </span>
  </div>
</template>

<style scoped>
.notification {
  position: absolute;
  top: 0;
  left: 0;

  display: flex;
  justify-content: center;
  align-items: center;
  

  width: 100%;
  height: 100%;
  background-color: #00000000;
  backdrop-filter:  brightness(60%);
  z-index: 20;

}

.notification span{


    display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;

    background-color: var(--color1);
    padding: 10px;
    border: solid 2px var(--color4);
    color: var(--color4);
}

.notification span button{

    border: none;
    width: 20%;
    height: 20px;
    color: var(--color1);

}
</style>
