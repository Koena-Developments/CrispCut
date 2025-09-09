<template>
  <div id="map" ref="mapContainer" class="w-full h-full z-0"></div>
</template>

<script setup>
import { onMounted, ref } from 'vue';

const mapContainer = ref(null);

const { data: artists, pending, error } = await useFetch('http://localhost:5007/api/artists/map-pins');

onMounted(async () => {
  const L = await import('leaflet');

  if (mapContainer.value) {
    const map = L.map(mapContainer.value).setView([-26.27, 27.85], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(map);

    // 2. Check if data was fetched successfully
    if (artists.value && artists.value.length > 0) {
      artists.value.forEach((artist) => {
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