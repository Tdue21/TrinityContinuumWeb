<script setup>

    const props = defineProps({
        value: {
            type: Number,
            required: true
        },
        maxDots: {
            type: Number,
            default: 5
        }
    });

    function updateValue(newValue) {
        this.$emit('value', newValue);
    }
</script>

<template>
    <div class="dots-input">

        <span class="dot-value" v-if="maxDots > 0">
            <span v-for="n in maxDots" :key="n" class="dot" @click="updateValue(n)">
                <template v-if="n <= value">&#x26AB;</template>
                <template v-else>&#x25EF;</template>
            </span>
        </span>
    </div>
</template>

<style scoped>
    .dots-input {
        display: grid;
        gap: 5px;
        grid-template-columns: 1fr 3fr;
        margin: 0px;
        padding: 0px;
    }

    .dot-value {
        display: flex;
        justify-content: flex-end;
        align-items: center;
        align-content: center;
        margin: 0px;
        padding: 0px;
    }   

    .dot {
        align-self: top;
        cursor: pointer;
        margin: 0px;
        padding: 0px;
    }
</style>