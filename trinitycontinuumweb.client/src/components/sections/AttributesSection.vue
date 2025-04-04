<template>
    <SectionHeading heading="Attributes"></SectionHeading>

    <div class="attributes">
        <div class="approach">
            <span><input type="checkbox" :checked="character.preferredApproach == 'Force'"> Force</span>
            <span><input type="checkbox" :checked="character.preferredApproach == 'Finesse'"> Finesse</span>
            <span><input type="checkbox" :checked="character.preferredApproach == 'Resilience'"> Resilience</span>
        </div>

        <div class="arena">
            <LabelValue label="Intellect" :showText=false :value="character.attributes.intellect" />
            <LabelValue label="Cunning"   :showText=false :value="character.attributes.cunning" />
            <LabelValue label="Resolve"   :showText=false :value="character.attributes.resolve" />

        </div>

        <div class="arena">
            <LabelValue label="Might"     :showText=false :value="character.attributes.might" />
            <LabelValue label="Dexterity" :showText=false :value="character.attributes.dexterity" />
            <LabelValue label="Stamina"   :showText=false :value="character.attributes.stamina" />

        </div>

        <div class="arena">
            <LabelValue label="Presence"     :showText=false :value="character.attributes.presence" />
            <LabelValue label="Manipulation" :showText=false :value="character.attributes.manipulation" />
            <LabelValue label="Composure"    :showText=false :value="character.attributes.composure" />

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
    });
</script>

<style scoped>
    .attributes {
        display: grid;
        grid-template-columns: 0.5fr 1fr 1fr 1fr;
        gap: 5px;
    }

    .approach {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        gap: 5px;
    }

    .arena {
        border: 1px solid black;
        padding: 5px;;
        display: flex;
        flex-direction: column;
        justify-items: stretch;
        gap: 5px;
    }
</style>