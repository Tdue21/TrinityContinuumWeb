<template>
    <div class="section">
        <div>
            <SectionHeading heading="Health"></SectionHeading>
            <div class="column">
                <template v-for="level in sorted">
                    <div class="injury-line">
                        <span><img src="../../assets/emptysquare.svg"></span>
                        <span>{{ level.name }}</span>
                        <span>{{ level.diff }}</span>
                    </div>
                </template>
                <div class="takenout"><span>Taken Out</span></div>
            </div>

        </div>

        <div>
            <SectionHeading heading="Aptitude"></SectionHeading>
            <div class="column">
                <LabelTextDots label="Aptitude" :text="character.aptitude.name" :maxDots="0" />
                <LabelTextDots label="Basic Powers" :text="character.aptitude.basicPowers" :maxDots="0" />

                <LabelTextDots v-for="mode in character.aptitude.modes" :label="mode.name" :value="mode.rating"
                    :showText="false" />
            </div>
            <SectionHeading heading="Psi &amp; Tolerance"></SectionHeading>
            <LabelTextDots label="PSI" :showText="false" :maxDots="10" />
        </div>
    </div>
</template>

<script setup>
    import character from '@/character';
    import LabelTextDots from '../comps/LabelTextDots.vue';
    import SectionHeading from '../comps/SectionHeading.vue';

    defineProps({

        character: {
            type: Object,
            required: true,
        }
    });


    let edges = character.edges;
    let levels = [
        { order: 0, name: "Bruised", diff: "+1" },
        { order: 1, name: "Injured", diff: "+2" },
        { order: 2, name: "Maimed", diff: "+4" }
    ];

    if (character.attributes.stamina >= 3) {
        levels.push({ order: 1, name: "Injured", diff: "+2" })
    };

    if (character.attributes.stamina >= 5) {
        levels.push({ order: 0, name: "Bruised", diff: "+1" });
    }

    if (Object.hasOwn(edges, "Endurance")) {
        levels.push({ order: 0, name: "Bruised", diff: "+1" });
    }

    let sorted = levels.sort((a, b) => a.order - b.order);
</script>

<style scoped>
    .section {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 15px;
    }

    .column {
        display: flex;
        flex-direction: column;
        gap: 5px;
    }

    .injury-line {        
        padding-left: 20px;
        padding-right: 20px;
        display: grid;
        grid-template-columns: 24px 1fr 24px;
        gap: 5px;
    }

    .injury-line img {
        width: 20px;
        height: 20px;
        margin: 0px;
        padding: 1px;
    }

    .takenout {
        text-align: center;
        font-weight: bold;
        font-size: 1.2rem;
    }

</style>