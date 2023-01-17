<script setup>
const props = defineProps({
  show: Boolean,
  z: {
    type : Number,
    default : 5
  }
});

const emit = defineEmits(['closeModal'])

const sendEvent = () => {
    props.show = false;
    emit("closeModal",false);
}
</script>

<template>
  <teleport to="body">
    <div class="modal" v-if="show" :style="{'z-index':z}">
      <span @click="sendEvent" class="material-symbols-outlined"> cancel_presentation </span>
       <slot></slot>
    </div>
  </teleport>
</template>

<style scoped>
.modal {
  position: absolute;
  top: 0;
  left: 0;

  display: flex;
  justify-content: center;
  align-items: center;

  width: 100%;
  height: 100%;
  background-color: #0000;
  backdrop-filter: brightness(60%) blur(2px);
  z-index: 5;
}

.modal span.material-symbols-outlined {
    position:absolute;
    top: 0;
    right: 0;

    cursor: pointer;

    font-size: 3rem;

}
</style>
