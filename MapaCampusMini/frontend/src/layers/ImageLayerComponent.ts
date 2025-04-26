import ImageLayer from 'ol/layer/Image';
import ImageStatic from 'ol/source/ImageStatic';
import { extent } from '../components/constants';

export const createImageLayer = () => {
    return new ImageLayer({
        source: new ImageStatic({
            url: '/imgMapa/mapa_unlam_modified.png',
            imageExtent: extent,
            projection: 'EPSG:3857',
        }),
    });
};
