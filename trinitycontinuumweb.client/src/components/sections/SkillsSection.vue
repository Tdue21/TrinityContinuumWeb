<template>
    <SectionHeading heading="Skills"></SectionHeading>

    <div class="section">
        <div class="column">
            <LabelValue label="Aim" :value="character.skills.aim" />
            <LabelValue label="Athletics" :value="character.skills.athletics" />
            <LabelValue label="Close Combat" :value="character.skills.closeCombat" />
            <LabelValue label="Command" :value="character.skills.command" />
            <LabelValue label="Culture" :value="character.skills.culture" />
            <LabelValue label="Empathy" :value="character.skills.empathy" />
            <LabelValue label="Enigmas" :value="character.skills.enigmas" />
            <LabelValue label="Humanities" :value="character.skills.humanities" />
        </div>

        <div class="column">
            <LabelValue label="Integrity" :value="character.skills.integrity" />
            <LabelValue label="Larceny" :value="character.skills.larceny" />
            <LabelValue label="Medicine" :value="character.skills.medicine" />
            <LabelValue label="Persuasion" :value="character.skills.persuasion" />
            <LabelValue label="Pilot" :value="character.skills.pilot" />
            <LabelValue label="Science" :value="character.skills.science" />
            <LabelValue label="Survival" :value="character.skills.survival" />
            <LabelValue label="Technology" :value="character.skills.technology" />
        </div>
    </div>
</template>

<script setup>
    import LabelValue from '../comps/LabelTextDots.vue';
    import SectionHeading from '../comps/SectionHeading.vue';

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
        gap: 5px;

    }

    .column {
        display: flex;
        flex-direction: column;
        justify-items: stretch;
        gap: 5px;
    }
</style>