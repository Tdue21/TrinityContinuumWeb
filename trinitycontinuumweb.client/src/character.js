


const character = {

    name: 'Connor McCormick',
    player: 'Thomas',
    concept: 'Private Investigator',
    psiOrder: 'ISRA',

    paths: {
        origin: { name: 'Street Rat', value: 1 },
        role: { name: 'Detective', value: 1 },
        society: { name: 'Æon Trinity', value: 1 },
    },

    skills: {
        aim: 2,
        athletics: 1,
        closeCombat: 1,
        command: 0,
        culture: 0,
        empathy: 0,
        enigmas: 3,
        humanities: 1,
        integrity: 1,
        larceny: 0,
        medicine: 0,
        persuasion: 2,
        pilot: 0,
        science: 0,
        survival: 3,
        technology: 1,
    },

    preferredApproach: 'finesse',

    attributes: {
        intellect: 3,
        cunning: 4,
        resolve: 3,

        might: 2,
        dexterity: 3,
        stamina: 3,

        presence: 2,
        manipulation: 2,
        composure: 2,
    },

    aptitude: {
        name: "Clairsentience",
        psi: 3,
        tolerance: 0,
        basic: ["Extended Attunement", "The Sight"],
        modes: [
            { name: "Psychometry", rating: 3 },
            { name: "Psycholocation", rating: 1 },
            { name: "Psychocognition", rating: 0 },
        ],
    },

    edges: {
        "Danger Sense": {
            value: 1
        },
        "Adrenaline Spike": {
            value: 1
        },
        "Photographic Memory": {
            value: 2
        },
        "Favored Mode": {
            note: "Psychometry",
            value: 2
        },
    }
};

export default character;
