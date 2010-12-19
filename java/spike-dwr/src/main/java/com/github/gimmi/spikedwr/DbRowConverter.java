package com.github.gimmi.spikedwr;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.StringTokenizer;
import java.util.TreeMap;

import org.directwebremoting.dwrp.ObjectOutboundVariable;
import org.directwebremoting.dwrp.ParseUtil;
import org.directwebremoting.dwrp.ProtocolConstants;
import org.directwebremoting.extend.ConverterManager;
import org.directwebremoting.extend.InboundContext;
import org.directwebremoting.extend.InboundVariable;
import org.directwebremoting.extend.MarshallException;
import org.directwebremoting.extend.NamedConverter;
import org.directwebremoting.extend.OutboundContext;
import org.directwebremoting.extend.OutboundVariable;
import org.directwebremoting.extend.Property;
import org.directwebremoting.extend.TypeHintContext;
import org.directwebremoting.util.LocalUtil;
import org.directwebremoting.util.Messages;
import org.json.JSONObject;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class DbRowConverter implements NamedConverter {
	private static final Logger logger = LoggerFactory.getLogger(DbRowConverter.class);
	private ConverterManager converterManager;
	private String javascript;

	@Override
	public Object convertInbound(@SuppressWarnings("rawtypes") Class paramType, InboundVariable iv, InboundContext inctx) throws MarshallException {
		logger.info("convertInbound(paramType={}, iv={}, inctx={})", new Object[] { paramType.getName(), iv.getValue(), inctx });
		String value = iv.getValue();
		
		return null;
		
//		// If the text is null then the whole bean is null
//		if (value.trim().equals(ProtocolConstants.INBOUND_NULL)) {
//			return null;
//		}
//
//		if (!value.startsWith(ProtocolConstants.INBOUND_MAP_START)) {
//			throw new MarshallException(paramType, Messages.getString("BeanConverter.FormatError", ProtocolConstants.INBOUND_MAP_START));
//		}
//
//		if (!value.endsWith(ProtocolConstants.INBOUND_MAP_END)) {
//			throw new MarshallException(paramType, Messages.getString("BeanConverter.FormatError", ProtocolConstants.INBOUND_MAP_START));
//		}
//
//		value = value.substring(1, value.length() - 1);
//		try {
//			DbRow dbRow = new DbRow(-1);
//			inctx.addConverted(iv, paramType, dbRow);
//
//			Map properties = getPropertyMapFromObject(dbRow, false, true);
//
//			// Loop through the properties passed in
//			Map<String, String> tokens = extractInboundTokens(paramType, value);
//			for(Map.Entry<String, String> entry : tokens.entrySet()) {
//				String key = entry.getKey();
//				String val = entry.getValue();
//
//				Property property = (Property) properties.get(key);
//
//				Class propType = property.getPropertyType();
//
//				String[] split = ParseUtil.splitInbound(val);
//				String splitValue = split[LocalUtil.INBOUND_INDEX_VALUE];
//				String splitType = split[LocalUtil.INBOUND_INDEX_TYPE];
//
//				InboundVariable nested = new InboundVariable(iv.getLookup(), null, splitType, splitValue);
//				TypeHintContext incc = createTypeHintContext(inctx, property);
//
//				Object output = converterManager.convertInbound(propType, nested, inctx, incc);
//				property.setValue(dbRow, output);
//			}
//
//			return dbRow;
//		} catch (MarshallException e) {
//			throw e;
//		} catch (Exception e) {
//			throw new MarshallException(paramType, e);
//		}
	}

    protected Map<String, String> extractInboundTokens(Class paramType, String value) throws MarshallException
    {
        Map<String, String> tokens = new HashMap<String, String>();
        StringTokenizer st = new StringTokenizer(value, ProtocolConstants.INBOUND_MAP_SEPARATOR);
        int size = st.countTokens();

        for (int i = 0; i < size; i++)
        {
            String token = st.nextToken();
            if (token.trim().length() == 0)
            {
                continue;
            }

            int colonpos = token.indexOf(ProtocolConstants.INBOUND_MAP_ENTRY);
            if (colonpos == -1)
            {
                throw new MarshallException(paramType, Messages.getString("BeanConverter.MissingSeparator", ProtocolConstants.INBOUND_MAP_ENTRY, token));
            }

            String key = token.substring(0, colonpos).trim();
            String val = token.substring(colonpos + 1).trim();
            tokens.put(key, val);
        }

        return tokens;
    }

	@Override
	public OutboundVariable convertOutbound(Object data, OutboundContext outctx) throws MarshallException {
		logger.info("convertOutbound(data={}, outctx={})", new Object[] { data, outctx });

		try {
			DbRow dbRow = (DbRow) data;
			// We need to do this before collecing the children to save recurrsion
			ObjectOutboundVariable ov = new ObjectOutboundVariable(outctx);
			outctx.put(data, ov);
			Map<String, OutboundVariable> ovs = new TreeMap<String, OutboundVariable>();
			for (Map.Entry<String, Object> column : dbRow.entrySet()) {
				OutboundVariable nested = converterManager.convertOutbound(column.getValue(), outctx);
				ovs.put(column.getKey(), nested);
			}
			ov.init(ovs, getJavascript());
			return ov;
		} catch (MarshallException e) {
			throw e;
		} catch (Exception e) {
			throw new MarshallException(data.getClass(), e);
		}
	}

	@Override
	public void setConverterManager(ConverterManager converterManager) {
		this.converterManager = converterManager;
	}

	@Override
	public Map getPropertyMapFromObject(Object example, boolean readRequired, boolean writeRequired) throws MarshallException {
		return getPropertyMapFromClass(example.getClass(), readRequired, writeRequired);
	}

	@Override
	public Map getPropertyMapFromClass(Class type, boolean readRequired, boolean writeRequired) throws MarshallException {
		return new HashMap();
	}

	@Override
	public Class getInstanceType() {
		return DbRow.class;
	}

	@Override
	public void setInstanceType(Class instanceType) {
		if (DbRow.class.equals(instanceType)) {
			throw new RuntimeException(String.format("Only %s type is supported", DbRow.class.getName()));
		}
	}

	@Override
	public String getJavascript() {
		return javascript;
	}

	@Override
	public void setJavascript(String javascript) {
		this.javascript = javascript;
	}
}
