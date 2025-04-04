<script setup>
    import { onMounted } from 'vue';
    import LogoSection from './components/sections/LogoSection.vue';
    import GeneralSection from './components/sections/GeneralSection.vue';
    import SkillsSection from './components/sections/SkillsSection.vue';
    import AttributesSection from './components/sections/AttributesSection.vue';
    import AdvantagesSection from './components/sections/AdvantagesSection.vue';
    import character from './character.js';

    // let character = null;

    // onMounted(async () => {
    //     try {
    //         var response = await fetch('/api/character/1');
    //         console.log(response);
    //         if(response.ok) {
    //             this.character = await response.json();
    //         }
    //         else {
    //             console.error('Failed to fetch character data');
    //         }
    //     }
    //     catch (error) {
    //         console.error('Failed to fetch character data', error);
    //     }
    // });
</script>

<template>
    <main>
        <LogoSection />
        <GeneralSection :character="character" />
        <SkillsSection :character="character" />
        <AttributesSection :character="character" />
        <AdvantagesSection :character="character" />
    </main>
</template>

<style scoped>

    @media (min-width: 1024px) {
        main {
            padding: 20px;
            border: 1px solid black;
            box-shadow: 5px 5px 5px #cfcfcf;
        }
    }
</style>
