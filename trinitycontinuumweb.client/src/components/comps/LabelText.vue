<script setup>
    const props = defineProps({
        label: {
            type: String,
            required: true
        },
        value: {
            type: String,
            default: ''
        },
    });

    function updateValue(newValue) {
        this.$emit('input', newValue);
    }
</script>

<template>
    <div class="labelled">
        <span>{{ label }}</span>
        <input type="text" :value="value" @keyup.enter="updateValue" />
    </div>
</template>

<style scoped>
    .labelled {
        display: grid;
        grid-template-columns: 1fr 3fr;
        margin: 0px;
        padding: 0px;
        gap: 5px;
    }

    .labelled span {
        font-weight: bold;
    }
</style>