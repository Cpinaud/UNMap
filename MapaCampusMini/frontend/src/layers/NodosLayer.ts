import VectorLayer from 'ol/layer/Vector';
import VectorSource from 'ol/source/Vector';
import GeoJSON from 'ol/format/GeoJSON';
import Style from 'ol/style/Style';
import CircleStyle from 'ol/style/Circle';
import Stroke from 'ol/style/Stroke';
import Fill from 'ol/style/Fill';

export const createNodosLayer = () => {
    return new VectorLayer({
        source: new VectorSource({
            url: '/jsonMapa/Nodos.geojson',
            format: new GeoJSON(),
        }),
        style: new Style({
            image: new CircleStyle({
                radius: 6,
                fill: new Fill({ color: 'red' }),
                stroke: new Stroke({ color: 'white', width: 2 }),
            }),
        }),
    });
};
