import VectorLayer from 'ol/layer/Vector';
import VectorSource from 'ol/source/Vector';
import GeoJSON from 'ol/format/GeoJSON';
import Style from 'ol/style/Style';
import Stroke from 'ol/style/Stroke';

export const createLineasLayer = () => {
    return new VectorLayer({
        source: new VectorSource({
            url: '/jsonMapa/Lineas.geojson',
            format: new GeoJSON(),
        }),
        style: new Style({
            stroke: new Stroke({
                color: 'blue',
                width: 3,
            }),
        }),
    });
};
