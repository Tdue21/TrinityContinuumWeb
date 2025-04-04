<script setup>
    import emptydot from '../../assets/emptydot.svg';

    const props = defineProps({
        label: {
            type: String,
            required: true
        },
        text: {
            type: String,
            default: ''
        },
        value: {
            type: Number,
            default: 0,
        },
        showText: {
            type: Boolean,
            default: true
        },

        maxDots: {
            type: Number,
            default: 5
        }
    });

    function updateText(event) {
        this.$emit('text', event.target.value);
    }   
    
    function updateValue(newValue) {
        this.$emit('value', newValue);
    }
</script>

<template>
    <div class="labelledInput" :class="{'with-dots' : (maxDots > 0 && showText == true)}">
        
        <span>{{ label }}</span>
        
        <input v-if="showText == true" type="text" :value="text" @keyup.enter="updateText" />

        <span class="dot-value" v-if="maxDots > 0">
            <span v-for="n in maxDots" :key="n" class="dot" @click="updateValue(n)">
                <template v-if="n <= value">
                    <img src="../../assets/filleddot.svg" alt="filleddot" />
                </template>
                <template v-else>
                    <img src="../../assets/emptydot.svg" alt="emptydot" />
                </template>
            </span>
        </span>
    </div>
</template>

<style scoped>
    
    .labelledInput {
        display: grid;
        gap: 5px;
        grid-template-columns: 1fr 3fr;
        margin: 0px;
        padding: 0px;
    }

    .with-dots {
        grid-template-columns: 1fr 2fr 1fr;
    }

    .dot-value {
        display: flex;
        justify-content: right;
        justify-items: right;
        text-align: right;
        margin: 0px;
        padding: 0px;
    }   
    
    .dot {
        align-self: top;
        cursor: pointer;
        margin: 0px;
        padding: 0px;
    }

    .dot>img {
        width: 20px;
        height: 20px;
        margin: 0px;
        padding: 1px;
    }
</style>