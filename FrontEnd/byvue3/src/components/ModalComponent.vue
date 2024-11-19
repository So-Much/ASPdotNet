<template>
  <div
      v-if="isOpen"
      @click="close"
      class="modal fade show"
      id="modalToggle"
      aria-labelledby="modalToggleLabel"
      tabindex="-1"
      aria-hidden="true"
      style="display: block;"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content" @click.stop>
        <div class="modal-header">
          <h5 class="modal-title" id="modalToggleLabel">Edit Blog {{ id }}</h5>
          <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
              @click="close"
          ></button>
        </div>
        <div class="modal-body">
          <slot></slot>
        </div>
        <div class="modal-footer">
          <button
              class="btn btn-primary"
              data-bs-target="#modalToggle2"
              data-bs-toggle="modal"
              data-bs-dismiss="modal"
              @click="saveChange"
          >
            Save changes
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, defineProps, defineEmits } from 'vue';

const props = defineProps({
  modelValue: {
    type: Boolean,
    required: true,
  },
  id: {
    type: String,
    required:true
  },
  onClosed: {
    type: Function,
    required: true
  }
});
const emit = defineEmits(["update:modelValue"]);

const isOpen = ref(props.modelValue);
watch(
    () => props.modelValue,
    ( newValue ) => {
      isOpen.value = newValue;
    }
);

const close = () => {
  isOpen.value = false;
  emit("update:modelValue", false);
};

const saveChange = () => {
  console.log("Save Changes");
  close();
  if (props?.onClosed) {
    props.onClosed();
  }
};
</script>

<style scoped>
@import '@/assets/vendor/css/core.css';
@import '@/assets/vendor/css/theme-default.css';
@import '@/assets/vendor/css/pages/page-auth.css';

</style>
