<script setup>
import { defineProps, onMounted } from 'vue';

const props = defineProps({
    // Normal property declarations
    btnName: String,
    calltofn: Function,
    // Styles property declarations
    styles: Object
});
onMounted(() => {
    for (const selector in props.styles) {
        document.documentElement.style.setProperty(selector, props.styles[selector]);
    }
});

</script>

<template>
    <button class="button" @click="calltofn">
        <svg xmlns="http://www.w3.org/2000/svg" width="1.25rem" height="1.25rem" viewBox="0 0 24 24" fill="none"
            stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2">
            <path d="M12 19v-7m0 0V5m0 7H5m7 0h7"></path>
        </svg>
        {{ btnName }}
    </button>

</template>

<style scoped>
/* From Uiverse.io by McHaXYT */
.button {
    cursor: pointer;
    font-size: 1rem;
    line-height: 1.5rem;
    padding: 0.625rem 1rem;
    color: rgb(242 242 242);
    background-color: rgb(79 70 229);
    background: linear-gradient(144deg, #af40ff, #5b42f3 50%, #00ddeb);
    font-weight: 600;
    border-radius: 0.5rem;
    border-style: none;
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 0.5rem;
    transition: all 0.35s linear;
    width: max-content;
}

.button:hover {
    box-shadow: inset 0 5px 25px 0 #af40ff, inset 0 10px 15px 0px #5b42f3,
        inset 0 5px 25px 0px #00ddeb;
}
</style>