<template>
    <div class="section">

            <LabelValue label="Name" :maxDots="0" :text="character.name" />
            <LabelValue label="Player" :maxDots="0" :text="character.player" />
            <LabelValue label="Concept" :maxDots="0" :text="character.concept"/>
            <LabelValue label="PSI Order" :maxDots="0" :text="character.psiOrder"/>

            <LabelValue label="Origin Path" :text="character.paths.origin.name" :value="character.paths.origin.value"/>
            <LabelValue label="Role Path" :text="character.paths.role.name" :value="character.paths.role.value"/>
            <LabelValue label="Society Path" :text="character.paths.society.name" :value="character.paths.society.value"/>

    </div>

</template>

<script setup>
    import LabelValue from '../comps/LabelTextDots.vue';

    defineProps({

        character: {
            type: Object,
            required: true,
        }
    })
</script>

<style scoped>
    .section {
        display: grid;
        grid-template-columns: 1fr 1fr;
        grid-template-rows: 1fr 1fr 1fr 1fr;
        grid-auto-flow: column;
        
        gap: 5px;

    }

</style>