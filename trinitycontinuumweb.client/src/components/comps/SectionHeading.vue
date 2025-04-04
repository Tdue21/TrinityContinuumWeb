<script setup>
defineProps({
  heading: {
    type: String,
    required: true,
  },
})
</script>

<template>
  <div class="heading">
      <h3>{{heading}}</h3>
  </div>
</template>

<style scoped>

.heading {
  margin-top: 10px;
  padding: 0px;
  background-color: #a4181c;
  color: white;
  text-align: center;
  width: 100%;
  margin-bottom: 10px;
}

.heading h3 {
  text-align: center;
  margin: 0;
  padding: 0px;
  font-size: 2.6rem;
  font-family: Blinddate;
}



</style>