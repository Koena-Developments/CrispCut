<template>
  <div id="map" ref="mapContainer" class="w-full h-full z-0"></div>
</template>

<script setup>
import { onMounted, ref } from 'vue';

const mapContainer = ref(null);

// 1. Fetch data from your backend API
const { data: artists, pending, error } = await useFetch('http://localhost:5007/api/artists/map-pins');

// onMounted ensures this code only runs on the client after the component is mounted
onMounted(async () => {
  // Dynamically import Leaflet only on the client-side
  const L = await import('leaflet');

  if (mapContainer.value) {
    const map = L.map(mapContainer.value).setView([-26.27, 27.85], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(map);

    // 2. Check if data was fetched successfully
    if (artists.value && artists.value.length > 0) {
      artists.value.forEach((artist) => {
        // Create a custom popup with more details
        const popupContent = `
          <div>
            <img src="${artist.imageUrl}" alt="${artist.fullName}" style="width: 50px; height: 50px; border-radius: 50%;">
            <b>${artist.fullName}</b><br>
            Category: ${artist.category}<br>
            Rating: ${artist.averageRating ? artist.averageRating.toFixed(1) : 'No ratings yet'}
          </div>
        `;

        const marker = L.marker([artist.locationLat, artist.locationLng]).addTo(map);
        marker.bindPopup(popupContent);
      });
    } else {
      console.error('No artist data found or failed to fetch.');
    }
  }
});
</script>